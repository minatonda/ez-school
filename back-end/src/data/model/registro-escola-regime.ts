import { Entity, OneToOne, Column, PrimaryGeneratedColumn, ManyToOne, OneToMany } from "typeorm";
import { ModelInterface } from "../model.interface";
import { Escola } from "./escola";
import { Aluno } from "./aluno";
import { RegistroEscolaCurso } from "./registro-escola-curso";
import { RegistroEscolaRegimeMateria } from "./registro-escola-regime-materia";

@Entity()
export class RegistroEscolaRegime implements ModelInterface {

    @PrimaryGeneratedColumn({type: "bigint"})
    id: number;

    @ManyToOne(type => Escola, escola => escola.listaRegistroEscolaAluno)
    registroEscolaCurso: RegistroEscolaCurso;

    @OneToMany(type => RegistroEscolaRegimeMateria, registroEscolaRegimeMateria => registroEscolaRegimeMateria.registroEscolaRegime)
    listaRegistroEscolaRegimeMateria: RegistroEscolaRegimeMateria[];

    @Column({ type: "date" })
    dataInicio: Date;
    
    @Column({ type: "date" })
    dataFim: Date;

}