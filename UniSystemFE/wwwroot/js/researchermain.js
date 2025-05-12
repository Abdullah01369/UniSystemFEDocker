 
        // Initialize tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl);
        });

        // Add hover effect to cards
        document.querySelectorAll('.stats-card, .profile-card, .publication-card, .research-card').forEach(card => {
        card.addEventListener('mouseenter', function () {
            this.style.boxShadow = '0 5px 15px rgba(0, 0, 0, 0.1)';
            this.style.transition = 'all 0.3s ease';
        });

    card.addEventListener('mouseleave', function() {
        this.style.boxShadow = '0 2px 4px rgba(0, 0, 0, 0.05)';
            });
        });

 
    document.addEventListener('DOMContentLoaded', function() {
        console.log('Loading collaboration data...');
         
        });
 