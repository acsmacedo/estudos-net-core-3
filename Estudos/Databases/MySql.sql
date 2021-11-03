/* 
	docker run -p 3307:3306 --name mysql-estudos -e MYSQL_ROOT_PASSWORD=admin -e MYSQL_USER=admin -e MYSQL_PASSWORD=admin -e MYSQL_DATABASE=estudos -d mysql:latest
*/

CREATE TABLE posts(
	id serial,
	user_id int,
	title varchar(255),
	body varchar(255)
);
