import { Entity, OneToOne, Column, PrimaryGeneratedColumn, ManyToOne, JoinTable, ManyToMany } from "typeorm";
import { ModelInterface } from "../model.interface";
import { RegistroEscolaCurso } from "./registro-escola-curso";
import { Materia } from "./materia";

@Entity()
export class Modulo implements ModelInterface {

    @PrimaryGeneratedColumn({type: "bigint"})
    id: number;

    @ManyToOne(type => RegistroEscolaCurso, registroEscolaCurso => registroEscolaCurso.listaModulo)
    registroEscolaCurso: RegistroEscolaCurso;

    @ManyToMany(type => Materia, materia => materia.listaModulo, {
        cascadeInsert: true,
        cascadeUpdate: true
    })
    @JoinTable()
    listaMateria: Materia[] = [];

}