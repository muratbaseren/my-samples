function setClick(selector) {
    $$(selector).on('click', function () {
        var link = $$(this);
        var id = link.data("id");
        var list = link.data("list");

        var dialog = app.dialog.create({
            title: 'Do Something..',
            text: 'Please select option to task item.',
            buttons: [
                {
                    text: 'Details',
                    color: "green",
                    onClick() {
                        app.dialog.alert(link.find(".item-title").text(), "Detail");
                    }
                },
                {
                    text: 'Edit',
                    color: "lime",
                    onClick() {
                        var defvalue = link.find(".item-title").text();
                        app.dialog.prompt(defvalue, "Edit", function (value) {
                            EditTodo(id, value, list);
                        }, null, defvalue);
                    }
                },
                {
                    text: 'Make Todo',
                    onClick() {
                        changeState(id, "0", list);
                    }
                },
                {
                    text: 'Completed',
                    onClick() {
                        changeState(id, "1", list);
                    }
                },
                {
                    text: 'Cancelled',
                    onClick() {
                        changeState(id, "2", list);
                    }
                },
                {
                    text: 'High Priority',
                    color: "deeporange",
                    onClick() {
                        changePriority(id, true, list);
                    }
                },
                {
                    text: 'Low Priority',
                    color: "orange",
                    onClick() {
                        changePriority(id, false, list);
                    }
                },
                {
                    text: 'Delete',
                    color: "red",
                    onClick() {
                        app.dialog.confirm("Are you sure want to delete this item?", "Delete Item?", function () {
                            DelTodo(id, list);
                        });
                    }
                },
            ],
            on: {
                open() {
                    console.log("dialog opened");
                },
                close() {
                    console.log("dialog close");
                }
            },
            verticalButtons: true,
            closeByBackdropClick: true,
        }).open();
    });
}

function changeState(id, state, list) {
    app.request.get("/Home/ChangeState/" + id + "?state=" + state, null, function (data) {
        app.request.get("/Home/GetTodoList?state=" + list, null, function (data) {
            $$('.' + list + '-list').html(data);
            setClick('.' + list + '-list .item-link');
        });

        app.request.get("/Home/GetTodoList?state=" + state, null, function (data) {
            var targetList = state === "0" ? "todo" : ((state === "1") ? "done" : "cancel");
            $$('.' + targetList + '-list').html(data);
            setClick('.' + targetList + '-list .item-link');
        });
    });
}

function changePriority(id, isImportant, list) {
    app.request.get("/Home/changePriority/" + id + "?isImportant=" + isImportant, null, function (data) {
        app.request.get("/Home/GetTodoList?state=" + list, null, function (data) {
            $$('.' + list + '-list').html(data);
            setClick('.' + list + '-list .item-link');
        });
    });
}

function DelTodo(id, list) {
    app.request.get("/Home/DeleteTodo/" + id, null, function (data) {
        app.request.get("/Home/GetTodoList?state=" + list, null, function (data) {
            $$('.' + list + '-list').html(data);
            setClick('.' + list + '-list .item-link');
        });
    });
}

function EditTodo(id, value, list) {
    app.request.post("/Home/EditTodo/" + id, { text: value }, function (data) {
        app.request.get("/Home/GetTodoList?state=" + list, null, function (data) {
            $$('.' + list + '-list').html(data);
            setClick('.' + list + '-list .item-link');
        });
    });
}

function setAddTodoHandler() {
    // Prompt
    $$('#add').on('click', function () {
        app.dialog.prompt('What do you want to do?', function (task) {
            app.request.post("/Home/AddTodo", { text: task }, function (data) {
                $$('.todo-list').html(data);
                setClick('.todo-list .item-link');
            }, function () {
                app.dialog.alert("Task couldn't be added.");
            });
        });
    });
}

function changeNavbarTitle(title) {
    $$(".navbar .title").text(title);
}


var swiper = app.swiper.get('.swiper-container');
swiper.on('slideChange', function () {
    //console.log('slide changed');
    changeNavbarTitle($$(".tab-link-active").data("title"));
});

setClick('.list.todos .item-link');
setAddTodoHandler();