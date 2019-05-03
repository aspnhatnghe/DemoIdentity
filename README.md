# Demo Identity
Buổi 20: Demo Identity

## 1. Sử dụng Individual User Accounts
* Tạo project
* Dựng database: Mở Packet Manager Console gõ lệnh: Update-Database
* Thực hiện đăng ký, đăng nhập và quan sát

## 2. Tự thiết kế phần Authentication
* Khai báo trong ConfigureServices():

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
                options.LoginPath = "/KhachHang/Login";
                options.LogoutPath = "/KhachHang/Logout";
                options.AccessDeniedPath = "/KhachHang/AccessDenied";
            });
            
* Khai báo sử dụng trong Configure():

            app.UseAuthentication();

* Tạo controller KhachHang và định nghĩa các action Login(), Logout(), Profile()

* Kiểm tra đã đăng nhập hay chưa trong View: 

            User.Identity.IsAuthenticated
