import { Entity, OneToOne, Column, PrimaryGeneratedColumn, ManyToOne, ManyToMany } from "typeorm";
import { ModelInterface } from "../model.interface";
import { Escola } from "./escola";
import { Aluno } from "./aluno";
import { Aula } from "./aula";

@Entity()
export class RegistroEscolaAluno implements ModelInterface {

    @PrimaryGeneratedColumn({type: "bigint"})
    id: number;

    @ManyToOne(type => Aluno, aluno => aluno.listaRegistroEscola)
    aluno: Aluno;

    @ManyToOne(type => Escola, escola => escola.listaRegistroEscolaAluno)
    escola: Escola;

    @ManyToMany(type => Aula, aula => aula.listaRegistroEscolaAlunoPresente, {
        cascadeInsert: true,
        cascadeUpdate: true
    })
    listaAulaPresente: Aula[] = [];

    @Column({ type: "date" })
    dataInicio: Date;
    
    @Column({ type: "date" })
    dataFim: Date;

}