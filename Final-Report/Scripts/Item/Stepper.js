const stepButtons = document.querySelectorAll('.step-button');
const progress = document.querySelector('#progress');

Array.from(stepButtons).forEach((button, index) => {
    button.addEventListener('click', () => {
        progress.setAttribute('value', index * 100 / (stepButtons.length - 1)); //there are 3 buttons. 2 spaces.

        stepButtons.forEach((item, secindex) => {
            if (index > secindex) {
                item.classList.add('done');
            }
            if (index < secindex) {
                item.classList.remove('done');
            }
        });

        const cardBody = document.querySelector(`#collapse${index + 1} .card-body`);
        const isCollapsed = cardBody.classList.contains('show');
        cardBody.style.height = isCollapsed ? '0' : `${cardBody.scrollHeight}px`;
    });
});