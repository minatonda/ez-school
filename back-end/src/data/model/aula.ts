import { Entity, PrimaryGeneratedColumn, Column, OneToOne, JoinColumn, ManyToMany, ManyToOne } from "typeorm";
import { Aluno } from "./aluno";
import { Professor } from "./professor";
import { Materia } from "./materia";
import { RegistroEscolaAluno } from "./registro-escola-aluno";
import { RegistroEscolaRegimeMateria } from "./registro-escola-regime-materia";
import { ModelInterface } from "../model.interface";

@Entity()
export class Aula implements ModelInterface {

    @PrimaryGeneratedColumn({type: "bigint"})
    id: number;

    @OneToOne(type => Professor)
    @JoinColumn()
    professorResponsavel: Professor;

    @ManyToOne(type => RegistroEscolaRegimeMateria, registroEscolaMateria => registroEscolaMateria.listaAula)
    registroEscolaMateria: RegistroEscolaRegimeMateria;

    @ManyToMany(type => RegistroEscolaAluno, registroEscolaAluno => registroEscolaAluno.listaAulaPresente, {
        cascadeInsert: true,
        cascadeUpdate: true
    })
    listaRegistroEscolaAlunoPresente: RegistroEscolaAluno[];

}