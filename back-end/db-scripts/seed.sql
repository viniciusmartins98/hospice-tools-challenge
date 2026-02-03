-- Generated randomly by gemini
-- 1. Insert Colors
-- We define specific UUIDs here so we can reference them easily in the patient inserts.

INSERT INTO color (id, name, hex_code) VALUES 
('10000000-0000-0000-0000-000000000001', 'Red', '#FF0000'),
('20000000-0000-0000-0000-000000000002', 'Green', '#00FF66'),
('30000000-0000-0000-0000-000000000003', 'Blue', '#0000FF'),
('40000000-0000-0000-0000-000000000004', 'Yellow', '#FFFF00'),
('50000000-0000-0000-0000-000000000005', 'Purple', '#800080'),
('60000000-0000-0000-0000-000000000006', 'Black', '#000000'),
('70000000-0000-0000-0000-000000000007', 'Pink', '#FF00FF');

-- 2. Insert Patients
-- These inserts use the UUIDs defined above to link favorite colors.
-- We use gen_random_uuid() for patient IDs (or you can use specific UUIDs if you prefer).

INSERT INTO patient (id, first_name, last_name, gender, age, favorite_color_id, created_at, updated_at) VALUES 
-- Patient 1: Loves Red
('a1b2c3d4-0001-4444-8888-000000000001', 'James', 'Smith', 'Male', 34, '10000000-0000-0000-0000-000000000001', NOW(), NULL),

-- Patient 2: Loves Green
('a1b2c3d4-0002-4444-8888-000000000002', 'Olivia', 'Johnson', 'Female', 28, '20000000-0000-0000-0000-000000000002', NOW(), NULL),

-- Patient 3: Loves Blue
('a1b2c3d4-0003-4444-8888-000000000003', 'Robert', 'Williams', 'Male', 45, '30000000-0000-0000-0000-000000000003', NOW(), NULL),

-- Patient 4: Loves Purple
('a1b2c3d4-0004-4444-8888-000000000004', 'Emily', 'Brown', 'Female', 22, '50000000-0000-0000-0000-000000000005', NOW(), NULL),

-- Patient 5: Loves Yellow
('a1b2c3d4-0005-4444-8888-000000000005', 'Michael', 'Jones', 'Male', 31, '40000000-0000-0000-0000-000000000004', NOW(), NULL),

-- Patient 6: Loves Pink
('a1b2c3d4-0006-4444-8888-000000000006', 'Sophia', 'Garcia', 'Female', 29, '70000000-0000-0000-0000-000000000007', NOW(), NULL),

-- Patient 7: Loves Black
('a1b2c3d4-0007-4444-8888-000000000007', 'William', 'Miller', 'Male', 52, '60000000-0000-0000-0000-000000000006', NOW(), NULL),

-- Patient 8: Loves Blue (Repeated color)
('a1b2c3d4-0008-4444-8888-000000000008', 'Isabella', 'Davis', 'Female', 25, '30000000-0000-0000-0000-000000000003', NOW(), NULL),

-- Patient 9: Loves Red (Repeated color)
('a1b2c3d4-0009-4444-8888-000000000009', 'David', 'Rodriguez', 'Male', 40, '10000000-0000-0000-0000-000000000001', NOW(), NULL),

-- Patient 10: Loves Purple (Repeated color)
('a1b2c3d4-0010-4444-8888-000000000010', 'Mia', 'Martinez', 'Female', 33, '50000000-0000-0000-0000-000000000005', NOW(), NULL);