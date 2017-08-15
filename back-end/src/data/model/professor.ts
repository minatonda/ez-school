import { Entity, OneToOne, Column, PrimaryGeneratedColumn, JoinColumn, OneToMany } from "typeorm";
import { ModelInterface } from "../model.interface";
import { RegistroEscolaProfessor } from "./registro-escola-professor";

@Entity()
export class Professor implements ModelInterface {

    @PrimaryGeneratedColumn({type: "bigint"})
    id: number;

    @Column({ type: "int" })
    pessoa: number;

    @OneToMany(type => RegistroEscolaProfessor, registroEscola => registroEscola.professor)
    listaRegistroEscola: RegistroEscolaProfessor[];

}