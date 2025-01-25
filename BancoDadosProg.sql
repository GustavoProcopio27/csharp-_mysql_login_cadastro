create database if not exists Banco_Cadastro;
use Banco_Cadastro;
create table if not exists Tbl_Funcionario(
							codigo int primary key Auto_Increment not null, 
							Nome varchar(100) not null,
							Endereco varchar(100) not null,
                            				Celular varchar(100) not null,
							Email varchar(100) not null,
							Funcao_Funcionario varchar(100) not null );

create table if not exists Tbl_Funcao(
						Id_Funcao int primary key auto_increment not null, 
						Funcao varchar(100) not null);
                        
insert into Tbl_Funcao(Funcao) values ("Gerente"),("Vendedor"),("Secretaria"),("Office-Boy");

select * from Tbl_Funcionario;

create table if not exists FormaPagar(id int auto_increment primary key, forma varchar(100));
Insert into formaPagar(forma) values("Pix"),("Cartão de crédito"), ("cartão de débito"), ("boleto"), ("Bitcoin");

create table if not exists tbl_venda(data date, codigo int auto_increment primary key, nome varchar(100), quantidade int, precoUnitario decimal (10,2), precoTotal decimal (10,2), formaPagamento varchar(100));





create table if not exists tbl_admin(id int primary key auto_increment,cpf varchar(100), nome varchar(100), senha varchar(200),salt varchar(100), master int);

insert into tbl_admin(cpf,nome,senha,salt,master) values ("207.706.107-30",  "gustavo",  "$2a$10$z0mwr3rzNIAH2dWx3bdeueDoCE67kUXsrGaigu8KDvY6XZ65yvLUK", "$2a$10$z0mwr3rzNIAH2dWx3bdeue",1);


