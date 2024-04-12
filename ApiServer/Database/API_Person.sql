DROP TABLE IF EXISTS api_person;
CREATE TABLE api_person (
  login TEXT DEFAULT NULL,
  password TEXT DEFAULT NULL,
  role TEXT DEFAULT NULL
);

INSERT INTO api_person (login, password, role) VALUES
('admin@gmail.com','12345','admin'),
('admin2@gmail.com','54321','admin'),
('qwerty@gmail.com','55555','user'),
('777@gmail.com','777','user'),
('www@gmail.com','05550','user');