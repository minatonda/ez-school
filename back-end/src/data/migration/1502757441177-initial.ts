import {Connection, EntityManager, MigrationInterface, QueryRunner} from "typeorm";

export class initial1502757441177 implements MigrationInterface {

    public async up(queryRunner: QueryRunner): Promise<any> {
        await queryRunner.query("CREATE TABLE `curso` (`id` bigint(20) NOT NULL PRIMARY KEY AUTO_INCREMENT, `nome` text NOT NULL, `descricao` text NOT NULL) ENGINE=InnoDB");
        await queryRunner.query("CREATE TABLE `modulo` (`id` bigint(20) NOT NULL PRIMARY KEY AUTO_INCREMENT, `registroEscolaCursoId` bigint(20)) ENGINE=InnoDB");
        await queryRunner.query("CREATE TABLE `materia` (`id` bigint(20) NOT NULL PRIMARY KEY AUTO_INCREMENT, `nome` text NOT NULL, `descricao` text NOT NULL) ENGINE=InnoDB");
        await queryRunner.query("CREATE TABLE `registro_escola_professor` (`id` bigint(20) NOT NULL PRIMARY KEY AUTO_INCREMENT, `dataInicio` date NOT NULL, `dataFim` date NOT NULL, `professorId` bigint(20), `escolaId` bigint(20)) ENGINE=InnoDB");
        await queryRunner.query("CREATE TABLE `professor` (`id` bigint(20) NOT NULL PRIMARY KEY AUTO_INCREMENT, `pessoa` int(11) NOT NULL) ENGINE=InnoDB");
        await queryRunner.query("CREATE TABLE `aula` (`id` bigint(20) NOT NULL PRIMARY KEY AUTO_INCREMENT, `professorResponsavelId` bigint(20), `registroEscolaMateriaId` bigint(20)) ENGINE=InnoDB");
        await queryRunner.query("CREATE TABLE `nota` (`id` bigint(20) NOT NULL PRIMARY KEY AUTO_INCREMENT, `observacao` text NOT NULL, `dataLancamento` date NOT NULL, `valor` int(11) NOT NULL, `registroEscolaMateriaId` bigint(20)) ENGINE=InnoDB");
        await queryRunner.query("CREATE TABLE `registro_escola_regime_materia` (`id` bigint(20) NOT NULL PRIMARY KEY AUTO_INCREMENT, `registroEscolaRegimeId` bigint(20), `materiaId` bigint(20)) ENGINE=InnoDB");
        await queryRunner.query("CREATE TABLE `registro_escola_regime` (`id` bigint(20) NOT NULL PRIMARY KEY AUTO_INCREMENT, `dataInicio` date NOT NULL, `dataFim` date NOT NULL, `registroEscolaCursoId` bigint(20)) ENGINE=InnoDB");
        await queryRunner.query("CREATE TABLE `registro_escola_curso` (`id` bigint(20) NOT NULL PRIMARY KEY AUTO_INCREMENT, `dataInicio` date NOT NULL, `dataFim` date NOT NULL, `regime` int(11) NOT NULL, `cursoId` bigint(20), `escolaId` bigint(20)) ENGINE=InnoDB");
        await queryRunner.query("CREATE TABLE `escola` (`id` bigint(20) NOT NULL PRIMARY KEY AUTO_INCREMENT, `nome` text NOT NULL, `cnpj` text NOT NULL) ENGINE=InnoDB");
        await queryRunner.query("CREATE TABLE `registro_escola_aluno` (`id` bigint(20) NOT NULL PRIMARY KEY AUTO_INCREMENT, `dataInicio` date NOT NULL, `dataFim` date NOT NULL, `alunoId` bigint(20), `escolaId` bigint(20)) ENGINE=InnoDB");
        await queryRunner.query("CREATE TABLE `aluno` (`id` bigint(20) NOT NULL PRIMARY KEY AUTO_INCREMENT, `pessoa` int(11) NOT NULL) ENGINE=InnoDB");
        await queryRunner.query("CREATE TABLE `pessoa` (`id` bigint(20) NOT NULL PRIMARY KEY AUTO_INCREMENT, `nome` text NOT NULL, `dataNascimento` date NOT NULL, `rg` text NOT NULL, `cpf` text NOT NULL, `alunoId` bigint(20), `professorId` bigint(20)) ENGINE=InnoDB");
        await queryRunner.query("CREATE TABLE `usuario` (`id` bigint(20) NOT NULL PRIMARY KEY AUTO_INCREMENT, `nomeusuario` text NOT NULL, `senha` text NOT NULL, `usuarioInfoId` bigint(20)) ENGINE=InnoDB");
        await queryRunner.query("CREATE TABLE `usuario_info` (`id` bigint(20) NOT NULL PRIMARY KEY AUTO_INCREMENT, `nome` text NOT NULL, `dataNascimento` date NOT NULL, `rg` text NOT NULL, `cpf` text NOT NULL, `usuarioId` bigint(20)) ENGINE=InnoDB");
        await queryRunner.query("CREATE TABLE `modulo_lista_materia_materia` (`moduloId` bigint(20) NOT NULL, `materiaId` bigint(20) NOT NULL, PRIMARY KEY(`moduloId`, `materiaId`)) ENGINE=InnoDB");
        await queryRunner.query("ALTER TABLE `pessoa` DROP `alunoId`");
        await queryRunner.query("ALTER TABLE `pessoa` DROP `professorId`");
        await queryRunner.query("ALTER TABLE `pessoa` ADD `alunoId` bigint(20)");
        await queryRunner.query("ALTER TABLE `pessoa` ADD `professorId` bigint(20)");
        await queryRunner.query("CREATE INDEX `ind_a0af1f8d54b2261370e2ef548a` ON `modulo_lista_materia_materia`(`moduloId`)");
        await queryRunner.query("CREATE INDEX `ind_54020a01b658dbaa7064e70c25` ON `modulo_lista_materia_materia`(`materiaId`)");
        await queryRunner.query("ALTER TABLE `modulo` ADD CONSTRAINT `fk_0707220b379553c47110cf05b55` FOREIGN KEY (`registroEscolaCursoId`) REFERENCES `registro_escola_curso`(`id`)");
        await queryRunner.query("ALTER TABLE `registro_escola_professor` ADD CONSTRAINT `fk_09554dc8c4c7e2db3ba0b0ba28c` FOREIGN KEY (`professorId`) REFERENCES `professor`(`id`)");
        await queryRunner.query("ALTER TABLE `registro_escola_professor` ADD CONSTRAINT `fk_88513dd5d382837d156c1f6708d` FOREIGN KEY (`escolaId`) REFERENCES `escola`(`id`)");
        await queryRunner.query("ALTER TABLE `aula` ADD CONSTRAINT `fk_b57d6d156d2f85cf77f31159aa3` FOREIGN KEY (`professorResponsavelId`) REFERENCES `professor`(`id`)");
        await queryRunner.query("ALTER TABLE `aula` ADD CONSTRAINT `fk_a67ab125aea9e24f802c35864c1` FOREIGN KEY (`registroEscolaMateriaId`) REFERENCES `registro_escola_regime_materia`(`id`)");
        await queryRunner.query("ALTER TABLE `nota` ADD CONSTRAINT `fk_a80f771177871fded2257f0b100` FOREIGN KEY (`registroEscolaMateriaId`) REFERENCES `registro_escola_regime_materia`(`id`)");
        await queryRunner.query("ALTER TABLE `registro_escola_regime_materia` ADD CONSTRAINT `fk_b48acb5ea301dd9019b7a1452c5` FOREIGN KEY (`registroEscolaRegimeId`) REFERENCES `registro_escola_regime`(`id`)");
        await queryRunner.query("ALTER TABLE `registro_escola_regime_materia` ADD CONSTRAINT `fk_b5c8c707fdd78d83e4592766d73` FOREIGN KEY (`materiaId`) REFERENCES `materia`(`id`)");
        await queryRunner.query("ALTER TABLE `registro_escola_regime` ADD CONSTRAINT `fk_0ce5e9351961f84a574839e0c98` FOREIGN KEY (`registroEscolaCursoId`) REFERENCES `escola`(`id`)");
        await queryRunner.query("ALTER TABLE `registro_escola_curso` ADD CONSTRAINT `fk_e781ca34efeb971b541c74c6af0` FOREIGN KEY (`cursoId`) REFERENCES `curso`(`id`)");
        await queryRunner.query("ALTER TABLE `registro_escola_curso` ADD CONSTRAINT `fk_e99611184c0c6c3d7b01e6f70b0` FOREIGN KEY (`escolaId`) REFERENCES `escola`(`id`)");
        await queryRunner.query("ALTER TABLE `registro_escola_aluno` ADD CONSTRAINT `fk_e18dbb4a1930192c4904d9732a9` FOREIGN KEY (`alunoId`) REFERENCES `aluno`(`id`)");
        await queryRunner.query("ALTER TABLE `registro_escola_aluno` ADD CONSTRAINT `fk_c12523ca60e9b4cfba2a2241095` FOREIGN KEY (`escolaId`) REFERENCES `escola`(`id`)");
        await queryRunner.query("ALTER TABLE `pessoa` ADD CONSTRAINT `fk_3d781e5554163173ff80fe6ceb0` FOREIGN KEY (`alunoId`) REFERENCES `aluno`(`id`)");
        await queryRunner.query("ALTER TABLE `pessoa` ADD CONSTRAINT `fk_bf923003d4b15628c7ddfa55875` FOREIGN KEY (`professorId`) REFERENCES `professor`(`id`)");
        await queryRunner.query("ALTER TABLE `usuario` ADD CONSTRAINT `fk_53718a29ffae0abcaed7b3740ed` FOREIGN KEY (`usuarioInfoId`) REFERENCES `usuario_info`(`id`)");
        await queryRunner.query("ALTER TABLE `usuario_info` ADD CONSTRAINT `fk_1ee822a0f1718f4f132c90e8bcc` FOREIGN KEY (`usuarioId`) REFERENCES `usuario`(`id`)");
        await queryRunner.query("ALTER TABLE `modulo_lista_materia_materia` ADD CONSTRAINT `fk_6bbda4d12ceb30e1d8d0a8bc177` FOREIGN KEY (`moduloId`) REFERENCES `modulo`(`id`)");
        await queryRunner.query("ALTER TABLE `modulo_lista_materia_materia` ADD CONSTRAINT `fk_fbf8aac0c62bfb9eaa3ce294878` FOREIGN KEY (`materiaId`) REFERENCES `materia`(`id`)");
    }

    public async down(queryRunner: QueryRunner): Promise<any> {
        await queryRunner.query("ALTER TABLE `modulo_lista_materia_materia` DROP FOREIGN KEY `fk_fbf8aac0c62bfb9eaa3ce294878`");
        await queryRunner.query("ALTER TABLE `modulo_lista_materia_materia` DROP FOREIGN KEY `fk_6bbda4d12ceb30e1d8d0a8bc177`");
        await queryRunner.query("ALTER TABLE `usuario_info` DROP FOREIGN KEY `fk_1ee822a0f1718f4f132c90e8bcc`");
        await queryRunner.query("ALTER TABLE `usuario` DROP FOREIGN KEY `fk_53718a29ffae0abcaed7b3740ed`");
        await queryRunner.query("ALTER TABLE `pessoa` DROP FOREIGN KEY `fk_bf923003d4b15628c7ddfa55875`");
        await queryRunner.query("ALTER TABLE `pessoa` DROP FOREIGN KEY `fk_3d781e5554163173ff80fe6ceb0`");
        await queryRunner.query("ALTER TABLE `registro_escola_aluno` DROP FOREIGN KEY `fk_c12523ca60e9b4cfba2a2241095`");
        await queryRunner.query("ALTER TABLE `registro_escola_aluno` DROP FOREIGN KEY `fk_e18dbb4a1930192c4904d9732a9`");
        await queryRunner.query("ALTER TABLE `registro_escola_curso` DROP FOREIGN KEY `fk_e99611184c0c6c3d7b01e6f70b0`");
        await queryRunner.query("ALTER TABLE `registro_escola_curso` DROP FOREIGN KEY `fk_e781ca34efeb971b541c74c6af0`");
        await queryRunner.query("ALTER TABLE `registro_escola_regime` DROP FOREIGN KEY `fk_0ce5e9351961f84a574839e0c98`");
        await queryRunner.query("ALTER TABLE `registro_escola_regime_materia` DROP FOREIGN KEY `fk_b5c8c707fdd78d83e4592766d73`");
        await queryRunner.query("ALTER TABLE `registro_escola_regime_materia` DROP FOREIGN KEY `fk_b48acb5ea301dd9019b7a1452c5`");
        await queryRunner.query("ALTER TABLE `nota` DROP FOREIGN KEY `fk_a80f771177871fded2257f0b100`");
        await queryRunner.query("ALTER TABLE `aula` DROP FOREIGN KEY `fk_a67ab125aea9e24f802c35864c1`");
        await queryRunner.query("ALTER TABLE `aula` DROP FOREIGN KEY `fk_b57d6d156d2f85cf77f31159aa3`");
        await queryRunner.query("ALTER TABLE `registro_escola_professor` DROP FOREIGN KEY `fk_88513dd5d382837d156c1f6708d`");
        await queryRunner.query("ALTER TABLE `registro_escola_professor` DROP FOREIGN KEY `fk_09554dc8c4c7e2db3ba0b0ba28c`");
        await queryRunner.query("ALTER TABLE `modulo` DROP FOREIGN KEY `fk_0707220b379553c47110cf05b55`");
        await queryRunner.query("ALTER TABLE `modulo_lista_materia_materia` DROP INDEX `ind_54020a01b658dbaa7064e70c25`");
        await queryRunner.query("ALTER TABLE `modulo_lista_materia_materia` DROP INDEX `ind_a0af1f8d54b2261370e2ef548a`");
        await queryRunner.query("ALTER TABLE `pessoa` DROP `professorId`");
        await queryRunner.query("ALTER TABLE `pessoa` DROP `alunoId`");
        await queryRunner.query("ALTER TABLE `pessoa` ADD `professorId` bigint(20)");
        await queryRunner.query("ALTER TABLE `pessoa` ADD `alunoId` bigint(20)");
        await queryRunner.query("DROP TABLE `modulo_lista_materia_materia`");
        await queryRunner.query("DROP TABLE `usuario_info`");
        await queryRunner.query("DROP TABLE `usuario`");
        await queryRunner.query("DROP TABLE `pessoa`");
        await queryRunner.query("DROP TABLE `aluno`");
        await queryRunner.query("DROP TABLE `registro_escola_aluno`");
        await queryRunner.query("DROP TABLE `escola`");
        await queryRunner.query("DROP TABLE `registro_escola_curso`");
        await queryRunner.query("DROP TABLE `registro_escola_regime`");
        await queryRunner.query("DROP TABLE `registro_escola_regime_materia`");
        await queryRunner.query("DROP TABLE `nota`");
        await queryRunner.query("DROP TABLE `aula`");
        await queryRunner.query("DROP TABLE `professor`");
        await queryRunner.query("DROP TABLE `registro_escola_professor`");
        await queryRunner.query("DROP TABLE `materia`");
        await queryRunner.query("DROP TABLE `modulo`");
        await queryRunner.query("DROP TABLE `curso`");
    }

}
