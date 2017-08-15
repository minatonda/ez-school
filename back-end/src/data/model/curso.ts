import { Entity, OneToOne, Column, PrimaryGeneratedColumn, OneToMany } from "typeorm";
import { RegistroEscolaCurso } from "./registro-escola-curso";
import { ModelInterface } from "../model.interface";

@Entity()
export class Curso implements ModelInterface {

    @PrimaryGeneratedColumn({type: "bigint"})
    id: number;

    @Column({ type: "text" })
    nome: string;

    @Column({ type: "text" })
    descricao: string;

    @OneToMany(type => RegistroEscolaCurso, registroEscola => registroEscola.curso)
    listaRegistroCurso: RegistroEscolaCurso[];

}