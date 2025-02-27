CREATE DATABASE Supermarket;
USE Supermarket;

CREATE TABLE NhaCungCap (
    id INT IDENTITY(1,1) PRIMARY KEY,
    maNhaCC NVARCHAR(50) NOT NULL UNIQUE,
    tenNhaCC NVARCHAR(255) NOT NULL,
    diaChi NVARCHAR(255) NOT NULL,
    createdAt DATETIME DEFAULT GETDATE(),
    updatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE ThoiTrang (
    id INT IDENTITY(1,1) PRIMARY KEY,
    maSP NVARCHAR(50) NOT NULL UNIQUE,
    tenSP NVARCHAR(255) NOT NULL,
    nhaCungCap NVARCHAR(50) NOT NULL,
    soLuong INT NOT NULL CHECK (soLuong >= 0),
    giaNhap DECIMAL(18,2) NOT NULL CHECK (giaNhap >= 0),
    giaBan DECIMAL(18,2) NOT NULL CHECK (giaBan >= 0),
    createdAt DATETIME DEFAULT GETDATE(),
    updatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (nhaCungCap) REFERENCES NhaCungCap(maNhaCC)
);

CREATE TABLE DienTu (
    id INT IDENTITY(1,1) PRIMARY KEY,
    maSP NVARCHAR(50) NOT NULL UNIQUE,
    tenSP NVARCHAR(255) NOT NULL,
    nhaCungCap NVARCHAR(50) NOT NULL,
    soLuong INT NOT NULL CHECK (soLuong >= 0),
    giaNhap DECIMAL(18,2) NOT NULL CHECK (giaNhap >= 0),
    giaBan DECIMAL(18,2) NOT NULL CHECK (giaBan >= 0),
    createdAt DATETIME DEFAULT GETDATE(),
    updatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (nhaCungCap) REFERENCES NhaCungCap(maNhaCC)
);

CREATE TABLE GiaDung (
    id INT IDENTITY(1,1) PRIMARY KEY,
    maSP NVARCHAR(50) NOT NULL UNIQUE,
    tenSP NVARCHAR(255) NOT NULL,
    nhaCungCap NVARCHAR(50) NOT NULL,
    soLuong INT NOT NULL CHECK (soLuong >= 0),
    giaNhap DECIMAL(18,2) NOT NULL CHECK (giaNhap >= 0),
    giaBan DECIMAL(18,2) NOT NULL CHECK (giaBan >= 0),
    createdAt DATETIME DEFAULT GETDATE(),
    updatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (nhaCungCap) REFERENCES NhaCungCap(maNhaCC)
);
 CREATE TABLE MyPham (
    id INT IDENTITY(1,1) PRIMARY KEY,
    maSP NVARCHAR(50) NOT NULL UNIQUE,
    tenSP NVARCHAR(255) NOT NULL,
    nhaCungCap NVARCHAR(50) NOT NULL,
    soLuong INT NOT NULL CHECK (soLuong >= 0),
    giaNhap DECIMAL(18,2) NOT NULL CHECK (giaNhap >= 0),
    giaBan DECIMAL(18,2) NOT NULL CHECK (giaBan >= 0),
    createdAt DATETIME DEFAULT GETDATE(),
    updatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (nhaCungCap) REFERENCES NhaCungCap(maNhaCC)
);
CREATE TABLE ThucPham (
    id INT IDENTITY(1,1) PRIMARY KEY,
    maSP NVARCHAR(50) NOT NULL UNIQUE,
    tenSP NVARCHAR(255) NOT NULL,
    nhaCungCap NVARCHAR(50) NOT NULL,
    soLuong INT NOT NULL CHECK (soLuong >= 0),
    giaNhap DECIMAL(18,2) NOT NULL CHECK (giaNhap >= 0),
    giaBan DECIMAL(18,2) NOT NULL CHECK (giaBan >= 0),
    createdAt DATETIME DEFAULT GETDATE(),
    updatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (nhaCungCap) REFERENCES NhaCungCap(maNhaCC)
);
CREATE TABLE DoChoi (
    id INT IDENTITY(1,1) PRIMARY KEY,
    maSP NVARCHAR(50) NOT NULL UNIQUE,
    tenSP NVARCHAR(255) NOT NULL,
    nhaCungCap NVARCHAR(50) NOT NULL,
    soLuong INT NOT NULL CHECK (soLuong >= 0),
    giaNhap DECIMAL(18,2) NOT NULL CHECK (giaNhap >= 0),
    giaBan DECIMAL(18,2) NOT NULL CHECK (giaBan >= 0),
    createdAt DATETIME DEFAULT GETDATE(),
    updatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (nhaCungCap) REFERENCES NhaCungCap(maNhaCC)
);
CREATE TABLE TheThao (
    id INT IDENTITY(1,1) PRIMARY KEY,
    maSP NVARCHAR(50) NOT NULL UNIQUE,
    tenSP NVARCHAR(255) NOT NULL,
    nhaCungCap NVARCHAR(50) NOT NULL,
    soLuong INT NOT NULL CHECK (soLuong >= 0),
    giaNhap DECIMAL(18,2) NOT NULL CHECK (giaNhap >= 0),
    giaBan DECIMAL(18,2) NOT NULL CHECK (giaBan >= 0),
    createdAt DATETIME DEFAULT GETDATE(),
    updatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (nhaCungCap) REFERENCES NhaCungCap(maNhaCC)
);
CREATE TABLE Sach (
    id INT IDENTITY(1,1) PRIMARY KEY,
    maSP NVARCHAR(50) NOT NULL UNIQUE,
    tenSP NVARCHAR(255) NOT NULL,
    nhaCungCap NVARCHAR(50) NOT NULL,
    soLuong INT NOT NULL CHECK (soLuong >= 0),
    giaNhap DECIMAL(18,2) NOT NULL CHECK (giaNhap >= 0),
    giaBan DECIMAL(18,2) NOT NULL CHECK (giaBan >= 0),
    createdAt DATETIME DEFAULT GETDATE(),
    updatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (nhaCungCap) REFERENCES NhaCungCap(maNhaCC)
);
CREATE TABLE ThuCung (
    id INT IDENTITY(1,1) PRIMARY KEY,
    maSP NVARCHAR(50) NOT NULL UNIQUE,
    tenSP NVARCHAR(255) NOT NULL,
    nhaCungCap NVARCHAR(50) NOT NULL,
    soLuong INT NOT NULL CHECK (soLuong >= 0),
    giaNhap DECIMAL(18,2) NOT NULL CHECK (giaNhap >= 0),
    giaBan DECIMAL(18,2) NOT NULL CHECK (giaBan >= 0),
    createdAt DATETIME DEFAULT GETDATE(),
    updatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (nhaCungCap) REFERENCES NhaCungCap(maNhaCC)
);
CREATE TABLE Thuoc (
    id INT IDENTITY(1,1) PRIMARY KEY,
    maSP NVARCHAR(50) NOT NULL UNIQUE,
    tenSP NVARCHAR(255) NOT NULL,
    nhaCungCap NVARCHAR(50) NOT NULL,
    soLuong INT NOT NULL CHECK (soLuong >= 0),
    giaNhap DECIMAL(18,2) NOT NULL CHECK (giaNhap >= 0),
    giaBan DECIMAL(18,2) NOT NULL CHECK (giaBan >= 0),
    createdAt DATETIME DEFAULT GETDATE(),
    updatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (nhaCungCap) REFERENCES NhaCungCap(maNhaCC)
);
CREATE TABLE MeVaBe (
    id INT IDENTITY(1,1) PRIMARY KEY,
    maSP NVARCHAR(50) NOT NULL UNIQUE,
    tenSP NVARCHAR(255) NOT NULL,
    nhaCungCap NVARCHAR(50) NOT NULL,
    soLuong INT NOT NULL CHECK (soLuong >= 0),
    giaNhap DECIMAL(18,2) NOT NULL CHECK (giaNhap >= 0),
    giaBan DECIMAL(18,2) NOT NULL CHECK (giaBan >= 0),
    createdAt DATETIME DEFAULT GETDATE(),
    updatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (nhaCungCap) REFERENCES NhaCungCap(maNhaCC)
);
CREATE TABLE GiaoDuc (
    id INT IDENTITY(1,1) PRIMARY KEY,
    maSP NVARCHAR(50) NOT NULL UNIQUE,
    tenSP NVARCHAR(255) NOT NULL,
    nhaCungCap NVARCHAR(50) NOT NULL,
    soLuong INT NOT NULL CHECK (soLuong >= 0),
    giaNhap DECIMAL(18,2) NOT NULL CHECK (giaNhap >= 0),
    giaBan DECIMAL(18,2) NOT NULL CHECK (giaBan >= 0),
    createdAt DATETIME DEFAULT GETDATE(),
    updatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (nhaCungCap) REFERENCES NhaCungCap(maNhaCC)
);
CREATE TABLE NhapKhau (
    id INT IDENTITY(1,1) PRIMARY KEY,
    maSP NVARCHAR(50) NOT NULL UNIQUE,
    tenSP NVARCHAR(255) NOT NULL,
    nhaCungCap NVARCHAR(50) NOT NULL,
    soLuong INT NOT NULL CHECK (soLuong >= 0),
    giaNhap DECIMAL(18,2) NOT NULL CHECK (giaNhap >= 0),
    giaBan DECIMAL(18,2) NOT NULL CHECK (giaBan >= 0),
    createdAt DATETIME DEFAULT GETDATE(),
    updatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (nhaCungCap) REFERENCES NhaCungCap(maNhaCC)
);
CREATE TABLE NhanVien (
    id INT IDENTITY(1,1) PRIMARY KEY,
    maNV NVARCHAR(50) NOT NULL UNIQUE,
    tenNV NVARCHAR(255) NOT NULL,
    soDT NVARCHAR(15) NOT NULL,
    diaChi NVARCHAR(255) NOT NULL,
    email NVARCHAR(255) NOT NULL UNIQUE,
    namSinh DATE CHECK (namSinh < GETDATE()),
    createdAt DATETIME DEFAULT GETDATE(),
    updatedAt DATETIME DEFAULT GETDATE()
);

-- INSERT dữ liệu nhà cung cấp
INSERT INTO NhaCungCap (maNhaCC, tenNhaCC, diaChi) 
VALUES ('MNCC01', N'Tiệm nhà Hấu', N'326 Nguyễn Trãi');

-- INSERT sản phẩm Thời Trang
INSERT INTO ThoiTrang (maSP, tenSP, nhaCungCap, soLuong, giaBan, giaNhap)
VALUES ('MASP001', N'Váy xinh mùa hè', 'MNCC01', 10, 250000, 280000);

INSERT INTO Thuoc(maSP, tenSP, nhaCungCap, soLuong, giaBan, giaNhap)
VALUES ('MASP001', N'Váy xinh mùa hè', 'MNCC01', 10, 250000, 280000);

INSERT INTO NhaCungCap(maNhaCC, tenNhaCC, diaChi)
VALUES ('MANCC001', N'Váy xinh mùa hè', 'MNCC01', 10, 250000, 280000);

-- Xem dữ liệu
SELECT * FROM ThoiTrang;

-- Cập nhật giá bán
UPDATE ThoiTrang  
SET giaBan = 290000, updatedAt = GETDATE()  
WHERE maSP = 'MASP001';

-- Xóa sản phẩm
DELETE FROM ThoiTrang  
WHERE maSP = 'MASP001';

-- Xuất hàng (giảm số lượng)
UPDATE ThoiTrang  
SET soLuong = soLuong - 8, updatedAt = GETDATE()  
WHERE maSP = 'MASP001' AND soLuong >= 8;
