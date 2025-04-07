-- Tabla Profesores
CREATE TABLE Profesores (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(100) NOT NULL
);

-- Tabla Materias
CREATE TABLE Materias (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(100) NOT NULL,
    Creditos INT NOT NULL DEFAULT 3,
    profesorId INT NOT NULL FOREIGN KEY REFERENCES profesores(Id)
);

-- Tabla Estudiantes
CREATE TABLE Estudiantes (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(100) NOT NULL,
    email NVARCHAR(100) NOT NULL UNIQUE,
    fechaCreacion DATETIME NOT NULL DEFAULT GETUTCDATE()
);

-- Tabla intermedia Estudiantes-Materias
CREATE TABLE EstudiantesMaterias (
    estudianteId INT NOT NULL FOREIGN KEY REFERENCES Estudiantes(Id) ON DELETE CASCADE,
    materiaId INT NOT NULL FOREIGN KEY REFERENCES Materias(Id) ON DELETE CASCADE,
    PRIMARY KEY (estudianteId, materiaId)
);

-- Datos iniciales
INSERT INTO Profesores (nombre) VALUES 
('Profesor García'), ('Profesora Martínez'), ('Profesor López'),
('Profesora Rodríguez'), ('Profesor Pérez');

INSERT INTO Materias (nombre, profesorId) VALUES
('Matemáticas', 1), ('Física', 1),
('Literatura', 2), ('Historia', 2),
('Química', 3), ('Biología', 3),
('Programación', 4), ('Bases de Datos', 4),
('Arte', 5), ('Música', 5);