// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// ===== Date Adder page script =====
document.addEventListener('DOMContentLoaded', function () {
    const startDate = document.getElementById('startDate');
    if (!startDate) return; // Only run on the Date Adder page

    startDate.addEventListener('input', function () {
        let v = this.value.replace(/\D/g, '');
        if (v.length > 2) v = v.slice(0, 2) + '/' + v.slice(2);
        if (v.length > 5) v = v.slice(0, 5) + '/' + v.slice(5);
        this.value = v.slice(0, 10);
    });

    document.addEventListener('keydown', e => { if (e.key === 'Enter') calculate(); });

    const btn = document.getElementById('calcBtn');
    if (btn) btn.addEventListener('click', calculate);
});

async function calculate() {
    const dateInput = document.getElementById('startDate');
    const daysInput = document.getElementById('days');
    const dateErr = document.getElementById('dateErr');
    const daysErr = document.getElementById('daysErr');
    const result = document.getElementById('result');
    const btn = document.getElementById('calcBtn');

    dateErr.textContent = '';
    daysErr.textContent = '';
    dateInput.classList.remove('error');
    daysInput.classList.remove('error');
    result.classList.remove('visible');

    let valid = true;

    if (!dateInput.value.trim()) {
        dateErr.textContent = 'Please enter a start date.';
        dateInput.classList.add('error');
        valid = false;
    }

    const days = parseInt(daysInput.value, 10);
    if (daysInput.value.trim() === '' || isNaN(days)) {
        daysErr.textContent = 'Enter a whole number (negative allowed).';
        daysInput.classList.add('error');
        valid = false;
    }

    if (!valid) return;

    btn.disabled = true;
    btn.textContent = 'Calculating…';

    try {
        const res = await fetch('/api/date/add-days', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ date: dateInput.value.trim(), days })
        });

        const data = await res.json();

        if (!res.ok) {
            const msg = data.error || 'Something went wrong.';
            if (msg.toLowerCase().includes('date')) {
                dateErr.textContent = msg; dateInput.classList.add('error');
            } else {
                daysErr.textContent = msg; daysInput.classList.add('error');
            }
            return;
        }

        document.getElementById('resultDate').textContent = data.newDate;
        const sign = data.daysAdded < 0 ? '−' : '+';
        const absDays = Math.abs(data.daysAdded);
        document.getElementById('resultMeta').textContent =
            `${data.originalDate} ${sign} ${absDays} day${absDays !== 1 ? 's' : ''}`;
        result.classList.add('visible');
    } catch {
        dateErr.textContent = 'Could not reach the server.';
    } finally {
        btn.disabled = false;
        btn.textContent = 'Calculate New Date';
    }
}
