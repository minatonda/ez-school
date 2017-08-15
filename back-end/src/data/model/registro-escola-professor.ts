import { Entity, OneToOne, Column, PrimaryGeneratedColumn, ManyToOne } from "typeorm";
import { ModelInterface } from "../model.interface";
import { Escola } from "./escola";
import { Professor } from "./professor";

@Entity()
export class RegistroEscolaProfessor implements ModelInterface {

    @PrimaryGeneratedColumn({type: "bigint"})
    id: number;

    @ManyToOne(type => Professor, professor => professor.listaRegistroEscola)
    professor: Professor;

    @ManyToOne(type => Escola, escola => escola.listaRegistroEscolaProfessor)
    escola: Escola;

    @Column({ type: "date" })
    dataInicio: Date;
    
    @Column({ type: "date" })
    dataFim: Date;

}