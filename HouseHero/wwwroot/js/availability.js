document.addEventListener('DOMContentLoaded', function() {
    const dayDropdown = document.getElementById('dayDropdown');
    const startTimeDropdown = document.getElementById('startTimeDropdown');
    const endTimeDropdown = document.getElementById('endTimeDropdown');
    const addToScheduleBtn = document.getElementById('addToScheduleBtn');
    const createAccountBtn = document.getElementById('createAccountBtn');
    const calendar = document.getElementById('calendar');
    const confirmationDialog = document.getElementById('confirmationDialog');
    const confirmYes = document.getElementById('confirmYes');
    const confirmNo = document.getElementById('confirmNo');

    const days = ['الأحد', 'الاثنين', 'الثلاثاء', 'الأربعاء', 'الخميس', 'الجمعة', 'السبت'];
    const hours = Array.from({length: 24}, (_, i) => `${i.toString().padStart(2, '0')}:00`);

    let selectedDay = '';
    let selectedStartTime = '';
    let selectedEndTime = '';
    let scheduleEvents = [];

    function initializeDropdowns() {
      initializeDropdown(dayDropdown, days);
      initializeDropdown(startTimeDropdown, hours);
      initializeDropdown(endTimeDropdown, hours);
    }

    function initializeDropdown(dropdown, items) {
      const header = dropdown.querySelector('.custom-dropdown-header span');
      const list = dropdown.querySelector('.custom-dropdown-list');
      const arrow = dropdown.querySelector('.oui-arrow-up');

      items.forEach(item => {
        const listItem = document.createElement('div');
        listItem.classList.add('custom-dropdown-item');
        listItem.textContent = item;
        listItem.addEventListener('click', () => {
          header.textContent = item;
          list.style.display = 'none';
          arrow.style.transform = 'rotate(0deg)';
          updateSelectedValues(dropdown, item);
        });
        list.appendChild(listItem);
      });

      dropdown.addEventListener('click', (e) => {
        if (!e.target.closest('.custom-dropdown-list')) {
          list.style.display = list.style.display === 'block' ? 'none' : 'block';
          arrow.style.transform = list.style.display === 'block' ? 'rotate(180deg)' : 'rotate(0deg)';
        }
      });
    }

    function updateSelectedValues(dropdown, value) {
      if (dropdown === dayDropdown) {
        selectedDay = value;
      } else if (dropdown === startTimeDropdown) {
        selectedStartTime = value;
        updateEndTimeDropdown();
      } else if (dropdown === endTimeDropdown) {
        selectedEndTime = value;
      }
      updateAddToScheduleButton();
    }

    function updateEndTimeDropdown() {
      const endTimeList = endTimeDropdown.querySelector('.custom-dropdown-list');
      endTimeList.innerHTML = '';
      const startIndex = hours.indexOf(selectedStartTime) + 1;
      hours.slice(startIndex).forEach(hour => {
        const listItem = document.createElement('div');
        listItem.classList.add('custom-dropdown-item');
        listItem.textContent = hour;
        listItem.addEventListener('click', () => {
          endTimeDropdown.querySelector('.custom-dropdown-header span').textContent = hour;
          endTimeList.style.display = 'none';
          endTimeDropdown.querySelector('.oui-arrow-up').style.transform = 'rotate(0deg)';
          updateSelectedValues(endTimeDropdown, hour);
        });
        endTimeList.appendChild(listItem);
      });
    }

    function updateAddToScheduleButton() {
      addToScheduleBtn.disabled = !(selectedDay && selectedStartTime && selectedEndTime);
    }

    function addEventToCalendar() {
      const event = { day: selectedDay, start: selectedStartTime, end: selectedEndTime };
      scheduleEvents.push(event);
      renderCalendarEvents();
      resetDropdowns();
    }

    function renderCalendarEvents() {
      // Clear existing events
      calendar.querySelectorAll('.calendar-event').forEach(el => el.remove());

      scheduleEvents.forEach(event => {
        const eventElement = document.createElement('div');
        eventElement.classList.add('calendar-event');
        eventElement.textContent = `${event.start} - ${event.end}`;
        eventElement.style.top = `${days.indexOf(event.day) * 36 + 20}px`;
        eventElement.style.left = '10px';
        eventElement.style.width = 'calc(100% - 20px)';

        eventElement.addEventListener('click', () => showConfirmationDialog(event));

        calendar.appendChild(eventElement);
      });
    }

    function resetDropdowns() {
      selectedDay = '';
      selectedStartTime = '';
      selectedEndTime = '';
      dayDropdown.querySelector('.custom-dropdown-header span').textContent = 'اليوم';
      startTimeDropdown.querySelector('.custom-dropdown-header span').textContent = 'من الساعة';
      endTimeDropdown.querySelector('.custom-dropdown-header span').textContent = 'حتى الساعة';
      updateAddToScheduleButton();
    }

    function showConfirmationDialog(event) {
      confirmationDialog.style.display = 'block';
      confirmYes.onclick = () => {
        removeEvent(event);
        confirmationDialog.style.display = 'none';
      };
      confirmNo.onclick = () => {
        confirmationDialog.style.display = 'none';
      };
    }

    function removeEvent(event) {
      scheduleEvents = scheduleEvents.filter(e => e !== event);
      renderCalendarEvents();
    }

    addToScheduleBtn.addEventListener('click', addEventToCalendar);

    createAccountBtn.addEventListener('click', () => {
      if (scheduleEvents.length > 0) {
        // Proceed with account creation
        console.log('Account creation logic here');
      } else {
        alert('يرجى إضافة فترة زمنية واحدة على الأقل قبل إنشاء الحساب');
      }
    });

    initializeDropdowns();
    function addEventToCalendar() {
        const event = { day: selectedDay, start: selectedStartTime, end: selectedEndTime };
        scheduleEvents.push(event);
        renderCalendarEvents();
        resetDropdowns();
        updateHiddenFields();
    }

    function updateHiddenFields() {
        scheduleEvents.forEach((event, index) => {
            document.querySelector(`input[name="SelectedDays[${index}]"]`).value = event.day;
            document.querySelector(`input[name="StartTimes[${index}]"]`).value = event.start;
            document.querySelector(`input[name="EndTimes[${index}]"]`).value = event.end;
        });
    }

    document.getElementById('availabilityForm').addEventListener('submit', function (e) {
        updateHiddenFields();
    });
  });