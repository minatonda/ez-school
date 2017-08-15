import { Entity, OneToOne, Column, PrimaryGeneratedColumn, JoinColumn, OneToMany } from "typeorm";
import { RegistroEscolaAluno } from "./registro-escola-aluno";
import { ModelInterface } from "../model.interface";

@Entity()
export class Aluno implements ModelInterface {

    @PrimaryGeneratedColumn({type: "bigint"})
    id: number;

    @Column({ type: "int" })
    pessoa: number;

    @OneToMany(type => RegistroEscolaAluno, registroEscola => registroEscola.aluno)
    listaRegistroEscola: RegistroEscolaAluno[];

}