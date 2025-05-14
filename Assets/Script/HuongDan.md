# Hướng Dẫn Sử Dụng A\* Pathfinding

## Tổng Quan

Dự án này là một hệ thống tìm đường sử dụng thuật toán A\* trong Unity. Hệ thống cho phép tạo các chướng ngại vật và tìm đường đi cho các agent từ điểm xuất phát đến điểm đích.

## Cấu Trúc Thư Mục

- **Agent**: Chứa script điều khiển agent di chuyển
- **Min Heap**: Cấu trúc dữ liệu heap tối thiểu để tối ưu hóa thuật toán A\*
- **ObstaclesBuilder**: Quản lý việc tạo và xử lý chướng ngại vật
- **PathFinding**: Chứa các script chính của thuật toán A\*
- **RequestManager**: Quản lý các yêu cầu tìm đường

## Luồng Chạy Chương Trình

### 1. Khởi Tạo

- `PointGrid`: Tạo lưới điểm trong không gian 3D
- `Astar_Manager`: Quản lý việc tìm đường
- `PathRequestManager`: Xử lý các yêu cầu tìm đường
- `ObstaclesBuilder`: Cho phép tạo chướng ngại vật

### 2. Tạo Chướng Ngại Vật

- Click chuột phải để tạo chướng ngại vật tại vị trí chuột
- Hệ thống sẽ tự động cập nhật lưới điểm xung quanh chướng ngại vật

### 3. Di Chuyển Agent

- Click chuột trái để đặt điểm đích cho agent
- Agent sẽ tự động tìm đường đi ngắn nhất đến điểm đích
- Sử dụng thuật toán A\* để tìm đường

## Chi Tiết Các Thành Phần

### Agent.cs

- Điều khiển di chuyển của agent
- Tốc độ di chuyển có thể điều chỉnh qua biến `speed`
- Khoảng cách dừng có thể điều chỉnh qua biến `stoppingDistance`

### Astar_Manager.cs

- Triển khai thuật toán A\*
- Sử dụng Heap để tối ưu hóa việc tìm kiếm
- Tính toán chi phí di chuyển giữa các điểm

### PointGrid.cs

- Quản lý lưới điểm trong không gian
- Tạo các điểm xung quanh chướng ngại vật
- Kiểm tra tính khả thi của đường đi

### PathRequestManager.cs

- Xử lý các yêu cầu tìm đường
- Hỗ trợ đa luồng (có thể bật/tắt qua biến `multiThreading`)
- Quản lý callback khi tìm đường hoàn tất

### Heap.cs

- Cấu trúc dữ liệu heap tối thiểu
- Tối ưu hóa việc tìm kiếm trong thuật toán A\*
- Hỗ trợ các thao tác: Add, Pop, Min, Contains

### ObstaclesBuilder.cs

- Cho phép tạo chướng ngại vật bằng chuột phải
- Tự động cập nhật lưới điểm khi có chướng ngại vật mới

## Cách Sử Dụng

1. **Thiết Lập Scene**:

   - Thêm `Astar_Manager` vào một GameObject
   - Thêm `PointGrid` vào cùng GameObject với `Astar_Manager`
   - Thêm `PathRequestManager` vào một GameObject riêng
   - Thêm `ObstaclesBuilder` vào một GameObject riêng
   - Thêm `Agent` vào GameObject cần di chuyển

2. **Tạo Chướng Ngại Vật**:

   - Click chuột phải để tạo chướng ngại vật
   - Hệ thống sẽ tự động cập nhật lưới điểm

3. **Di Chuyển Agent**:
   - Click chuột trái để đặt điểm đích
   - Agent sẽ tự động tìm đường và di chuyển

## Lưu Ý

- Đảm bảo các GameObject có layer phù hợp (layer 8 cho chướng ngại vật)
- Có thể điều chỉnh các thông số như tốc độ di chuyển, khoảng cách dừng trong Inspector
- Hệ thống hỗ trợ đa luồng, có thể bật/tắt qua biến `multiThreading` trong `PathRequestManager`
