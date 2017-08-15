import { Entity, PrimaryGeneratedColumn, Column, OneToMany } from "typeorm";
import { RegistroEscolaAluno } from "./registro-escola-aluno";
import { RegistroEscolaCurso } from "./registro-escola-curso";
import { RegistroEscolaProfessor } from "./registro-escola-professor";
import { ModelInterface } from "../model.interface";

@Entity()
export class Escola implements ModelInterface {

    @PrimaryGeneratedColumn({type: "bigint"})
    id: number;

    @Column({ type: "text" })
    nome: string;

    @Column({ type: "text" })
    cnpj: string;

    @OneToMany(type => RegistroEscolaAluno, registroEscolaAluno => registroEscolaAluno.escola)
    listaRegistroEscolaAluno: RegistroEscolaAluno[];

    @OneToMany(type => RegistroEscolaProfessor, registroEscolaProfessor => registroEscolaProfessor.escola)
    listaRegistroEscolaProfessor: RegistroEscolaProfessor[];

    @OneToMany(type => RegistroEscolaCurso, registroEscolaCurso => registroEscolaCurso.escola)
    listaRegistroEscolaCurso: RegistroEscolaCurso[];

}