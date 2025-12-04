function toggleEditMode() {
    const container = document.querySelector('.profile-container');
    const displayMode = container.querySelector('.display-mode');
    const editMode = container.querySelector('.edit-mode');
    const toggleButton = container.querySelector('.edit-toggle-btn');

    // Kiểm tra trạng thái hiện tại
    const isEditing = editMode.classList.contains('visible');

    if (isEditing) {
        // Chuyển sang chế độ Hiển thị (Display Mode)
        displayMode.classList.remove('hidden');
        displayMode.classList.add('visible');
        editMode.classList.remove('visible');
        editMode.classList.add('hidden');
        toggleButton.textContent = 'Chỉnh sửa thông tin cá nhân';
        toggleButton.style.backgroundColor = '#28a745';

    } else {
        // Chuyển sang chế độ Chỉnh sửa (Edit Mode)
        displayMode.classList.remove('visible');
        displayMode.classList.add('hidden');
        editMode.classList.remove('hidden');
        editMode.classList.add('visible');
        toggleButton.textContent = 'Đóng chế độ chỉnh sửa';
        toggleButton.style.backgroundColor = '#dc3545'; // Đổi màu nút khi ở chế độ Edit
    }
}

// Lắng nghe sự kiện submit (Đây là nơi bạn gọi API để cập nhật dữ liệu)
document.getElementById('personal-edit-form').addEventListener('submit', function (event) {
    event.preventDefault();
    alert('Dữ liệu đã sẵn sàng để gửi lên server! (Cần code xử lý .NET)');
    // Sau khi gửi thành công, bạn nên gọi lại toggleEditMode() để đóng form.
    // toggleEditMode();
});


// Hàm hiển thị/ẩn bảng chi tiết điện nước
function showUtilityDetail() {
    const utilityDetailSection = document.querySelector('.detail-section');
    const isVisible = utilityDetailSection.style.display === 'block';

    if (isVisible) {
        utilityDetailSection.style.display = 'none';
        document.querySelector('.detail-btn').textContent = 'Xem chi tiết';
    } else {
        utilityDetailSection.style.display = 'block';
        document.querySelector('.detail-btn').textContent = 'Thu gọn chi tiết';
        // Cuộn tới phần chi tiết
        utilityDetailSection.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
}

// Hàm mở Modal thanh toán (Cần xây dựng Modal HTML/CSS)
function openPaymentModal() {
    // Trong thực tế, bạn sẽ dùng thư viện Modal (Bootstrap, jQuery UI...)
    const modal = document.getElementById('paymentModal');
    if (modal) {
        modal.style.display = 'block';
        alert('Chức năng Thanh toán đang được mở. Trong thực tế, bạn sẽ thấy Modal chứa cổng thanh toán ở đây.');
    }

    // Đây là nơi bạn khởi tạo tích hợp cổng thanh toán (Payment Gateway)
}

// Hàm đóng Modal (cần thêm vào Modal HTML)
function closeModal() {
    const modal = document.getElementById('paymentModal');
    if (modal) {
        modal.style.display = 'none';
    }
}

document.addEventListener('DOMContentLoaded', function () {
    // Đảm bảo chỉ tab đầu tiên được hiển thị khi tải trang
    document.getElementById('repair-request').style.display = 'block';
});

function openTab(evt, tabName) {
    let i, tabcontent, tablinks;

    // Lấy tất cả elements với class="tab-content" và ẩn chúng
    tabcontent = document.getElementsByClassName("tab-content");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
        tabcontent[i].classList.remove('active-tab'); // Loại bỏ class active
    }

    // Lấy tất cả elements với class="tab-link" và loại bỏ class active
    tablinks = document.getElementsByClassName("tab-link");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].classList.remove("active");
    }

    // Hiển thị tab hiện tại, thêm class "active" cho nút đã nhấn
    document.getElementById(tabName).style.display = "block";
    document.getElementById(tabName).classList.add('active-tab');
    evt.currentTarget.classList.add("active");
}

function submitRequest(event, requestType) {
    event.preventDefault(); // Ngăn chặn form submit thực tế

    // Thu thập dữ liệu form ở đây...

    alert(`Yêu cầu '${requestType}' đã được gửi thành công! Ban quản lý sẽ sớm xử lý.`);

    // Trong thực tế:
    // 1. Gửi dữ liệu bằng AJAX/fetch đến Controller .NET
    // 2. Sau khi thành công, chuyển sinh viên sang Tab Trạng Thái Yêu Cầu.

    // Ví dụ chuyển sang Tab Trạng Thái:
    // document.querySelector('.tab-link:nth-child(4)').click(); 
}

function registerService(serviceName) {
    if (confirm(`Bạn có chắc chắn muốn đăng ký dịch vụ: ${serviceName}? Chi phí sẽ được tính vào hóa đơn hàng tháng.`)) {
        alert(`Đăng ký dịch vụ ${serviceName} thành công!`);
        // Trong thực tế: Cần gửi yêu cầu đăng ký lên server và cập nhật trạng thái nút.
    }
}