const observer = new IntersectionObserver(entries => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            const elements = document.querySelectorAll('.container_text_inside');
            elements[0].classList.add('fadeInLeft');
            elements[1].classList.add('fadeInUp');
        }
    });
});

// Observe the container div
observer.observe(document.querySelector('#container_text'));
