CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS keeps(
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    creatorId VARCHAR(255) NOT NULL,
    name TEXT NOT NULL,
    description TEXT NOT NULL,
    img TEXT NOT NULL,
    views INT DEFAULT 0,

    FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) DEFAULT CHARSET UTF8;

CREATE TABLE IF NOT EXISTS vaults(
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creatorId VARCHAR(255) NOT NULL,
    name TEXT NOT NULL,
    description TEXT NOT NULL,
    isPrivate TINYINT NOT NULL,

    FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) DEFAULT CHARSET UTF8;

CREATE TABLE IF NOT EXISTS vaultkeeps(
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    creatorId VARCHAR(255) NOT NULL,
    vaultId INT NOT NULL,
    keepId INT NOT NULL,

    FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
    FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE,
    FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE
) DEFAULT CHARSET UTF8;

SELECT
k.*,
                COUNT(k.id = keepId) AS kept,
                a.*
            FROM keeps k
            LEFT JOIN vaultkeeps v ON k.id = v.keepId
            JOIN accounts a ON k.creatorId = a.id
            GROUP BY k.id;

            INSERT INTO vaultkeeps (creatorId, vaultId, keepId) VALUES ("62755286701a57ffa8c287de", 2, 1);