document.addEventListener('DOMContentLoaded', function () {
    const panelHeaders = document.querySelectorAll('.panel-header');

    panelHeaders.forEach(header => {
        header.addEventListener('click', function () {
            const panel = this.parentElement;
            const content = this.nextElementSibling;
            const toggle = this.querySelector('.panel-toggle');

            if (content.style.display === 'block') {
                content.style.display = 'none';
                toggle.textContent = '+';
            } else {
                content.style.display = 'block';
                toggle.textContent = '−';
            }
        });
    });
});