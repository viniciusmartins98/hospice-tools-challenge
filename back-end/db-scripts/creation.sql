create table color(
  id uuid primary key,
  name varchar(100) not null,
  hex_code varchar(7) not null
);

create table patient(
  id uuid primary key,
  first_name varchar(50) not null,
  last_name varchar(50) not null,
  gender varchar(10),
  age int,
  favorite_color_id uuid,
  created_at timestamp not null,
  updated_at timestamp,
  constraint fk_patient_color foreign key(favorite_color_id) references color(id)
);