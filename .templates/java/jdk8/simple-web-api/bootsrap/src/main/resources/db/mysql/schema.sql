CREATE TABLE `profile`
(
    `id`          VARCHAR(36)  NOT NULL PRIMARY KEY,
    `title`       VARCHAR(50)  NOT NULL,
    `description` VARCHAR(500) NOT NULL,
);

CREATE TABLE `license`
(
    `id`          VARCHAR(36)  NOT NULL PRIMARY KEY,
    `title`       VARCHAR(50)  NOT NULL,
    `url`         VARCHAR(500) NOT NULL,
);