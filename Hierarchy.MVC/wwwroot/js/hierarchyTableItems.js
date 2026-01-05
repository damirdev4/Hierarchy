let multipleAddChildButtonElements = document.querySelectorAll('.add-child');
let multipleRemoveChildButtonElements = document.querySelectorAll('a.delete');

const exampleModal = document.querySelector('#exampleModal');
const modalSaveButton = exampleModal.querySelector('.modal-footer .btn-primary');
let addEditButton = null;
let modalBodyInput = null;
let modalInstance = null;
let form = null;
let id = null;
let operation = null;
let nameSpan = null; 

multipleAddChildButtonElements.forEach(element => {
    element.addEventListener('click', addChildItems);
});
multipleRemoveChildButtonElements.forEach(element => {
    element.addEventListener('click', removeItem);
});
function addChildItems(event) {
    const button = event.currentTarget;
    const icon = button.querySelector("i");
    const buttonId = event.currentTarget.id;
    const parentNode = event.currentTarget.closest("li");
    const ul = parentNode.querySelector('ul');

    if (ul && ul.querySelector("li")) {
        if (icon.classList.contains("bi-plus")) {
            icon.classList.remove("bi-plus")
            icon.classList.add("bi-dash")
            ul.hidden = false;
        }
        else {
            icon.classList.remove("bi-dash")
            icon.classList.add("bi-plus")
            ul.hidden = true;
        }
    }
    else {
        $.ajax({
            type: 'GET',
            url: '/HierarchyTableItems/GetChildItems/' + buttonId,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.includes("li")) {
                    icon.classList.remove("bi-plus");
                    icon.classList.add("bi-dash");
                    parentNode.innerHTML += data;
                    multipleAddChildButtonElements = document.querySelectorAll('.add-child');
                    multipleAddChildButtonElements.forEach(element => {
                        element.addEventListener('click', addChildItems);
                    });
                    multipleRemoveChildButtonElements = document.querySelectorAll('a.delete');
                    multipleRemoveChildButtonElements.forEach(element => {
                        element.addEventListener('click', removeItem);
                    });
                }
            },
            error: function (error) {
                console.error("Error:", error);
            }
        });
    }
}

function removeItem(event) {
    const li = event.currentTarget.closest("li");
    const ul = li.closest("ul");
    const icon = ul.closest("li").querySelector("i");

    if (ul.querySelectorAll(':scope > li').length == 1) {
        icon.classList.remove("bi-dash");
        icon.classList.add("bi-plus");
    }

    const id = event.currentTarget.getAttribute('data-bs-id')
    li.remove();
    $.ajax({
        type: 'DELETE',
        url: '/HierarchyTableItems/RemoveItem/' + id,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
        },
        error: function (error) {
            console.error("Error:", error);
        }
    });

    multipleAddChildButtonElements = document.querySelectorAll('.add-child');
    multipleAddChildButtonElements.forEach(element => {
        element.addEventListener('click', addChildItems);
    });
    multipleRemoveChildButtonElements = document.querySelectorAll('a.delete');
    multipleRemoveChildButtonElements.forEach(element => {
        element.addEventListener('click', removeItem);
    });
}

exampleModal.addEventListener('show.bs.modal', event => {
    modalInstance = bootstrap.Modal.getInstance(exampleModal);
    addEditButton = event.relatedTarget;
    id = addEditButton.getAttribute('data-bs-id');
    operation = addEditButton.getAttribute('data-bs-op');
    const modalTitle = exampleModal.querySelector('.modal-title');
    modalBodyInput = exampleModal.querySelector('.modal-body input');

    nameSpan = document.querySelector('#span-' + id);
    form = document.querySelector('#modalForm');
    modalTitle.textContent = addEditButton.textContent;

    if (operation == "change") {
        modalBodyInput.value = nameSpan.textContent;
    }
    else {
        modalBodyInput.value = "";
    }
})

modalSaveButton.onclick = function (event) {
    operation = addEditButton.getAttribute('data-bs-op');
    id = addEditButton.getAttribute('data-bs-id');
    nameSpan = document.querySelector('#span-' + id);
    form = document.querySelector('#modalForm');
    form.classList.add('was-validated');
    if (form.checkValidity()) {
        if (operation == "change") {
            $.ajax({
                type: 'PUT',
                url: '/HierarchyTableItems/UpdateItem',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({
                    Id: id,
                    Name: modalBodyInput.value
                }),
                success: function (data) {
                    nameSpan.textContent = modalBodyInput.value;
                    modalInstance.hide();
                },
                error: function (error) {
                    console.error("Error:", error);
                }
            });
        }
        else {
            $.ajax({
                type: 'POST',
                url: '/HierarchyTableItems/CreateItem',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({
                    Name: modalBodyInput.value,
                    ParentId: id
                }),
                success: function (id) {
                    $.ajax({
                        type: 'GET',
                        url: '/HierarchyTableItems/GetItem/' + id,
                        contentType: 'application/json; charset=utf-8',
                        success: function (html) {

                            const li = addEditButton.closest("li");
                            const ul = li.querySelector('ul');
                            const icon = li.querySelector("i");

                            if (ul) {
                                ul.innerHTML += html;

                                if (icon.classList.contains("bi-plus")) {
                                    icon.classList.add("bi-dash");
                                    icon.classList.remove("bi-plus");
                                    ul.hidden = false;
                                }
                            }
                            else {
                                icon.classList.remove("bi-plus");
                                icon.classList.add("bi-dash");
                                const newUl = document.createElement("ul");
                                newUl.innerHTML += html;
                                li.appendChild(newUl);
                            }

                            multipleAddChildButtonElements = document.querySelectorAll('.add-child');
                            multipleAddChildButtonElements.forEach(element => {
                                element.addEventListener('click', addChildItems);
                            });
                            multipleRemoveChildButtonElements = document.querySelectorAll('a.delete');
                            multipleRemoveChildButtonElements.forEach(element => {
                                element.addEventListener('click', removeItem);
                            });
                            modalInstance.hide();
                        },
                        error: function (error) {
                            console.error("Error:", error);
                        }
                    });
                },
                error: function (error) {
                    console.error("Error:", error);
                }
            });
        }
    }
}

