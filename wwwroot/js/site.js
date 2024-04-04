document.getElementById('accessType').addEventListener('change', function () {
    var element = document.querySelector('.nonelabel');

    if (this.value === 'temporary') {
        element.style.visibility = 'visible';
    }
    else {
        element.style.visibility = 'hidden';
    }
});