$(document).ready(function () {
    $('form').submit(function () {
        var name = $('#Name').val();
        if (name.trim() === '') {
            alert('Task Name is required.');
            return false;
        }
        return true;
    });

    $('.deletetask').click(function () {
        var result = confirm('Do you want to delete the Task?');
        if (result) {
            return true;
        }
        else {
            return false;
        }
    });
});