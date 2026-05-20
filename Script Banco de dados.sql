CREATE DATABASE IF NOT EXISTS db_exoapi;
USE db_exoapi;

CREATE TABLE tb_projetos (
	cd_projeto INT AUTO_INCREMENT PRIMARY KEY,
    nm_projeto VARCHAR(150) NOT NULL,
    nm_area VARCHAR(150) NOT NULL,
    fl_status BOOLEAN
);

INSERT INTO tb_projetos (nm_projeto, nm_area, fl_status)
VALUES ('Projeto A - Obras BR' , 'Construção Civil' , 1) ;

INSERT INTO tb_projetos (nm_projeto, nm_area, fl_status)
VALUES ('Projeto B - Senai Fest' , 'Eventos' , 0);

INSERT INTO tb_projetos (nm_projeto, nm_area, fl_status)
VALUES ('Projeto C - Hackathon Fest' , 'Eventos' , 1) ;

CREATE TABLE tb_usuarios (
cd_usuario INT AUTO_INCREMENT PRIMARY KEY,
ds_email VARCHAR(255) NOT NULL UNIQUE,
ds_senha VARCHAR(120) NOT NULL
);

INSERT INTO tb_usuarios (ds_email, ds_senha)
VALUES ('email@sp.br' , '1234') , ('email_dois@sp.br' , '1234');