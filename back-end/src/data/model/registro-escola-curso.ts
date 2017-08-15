import { Entity, OneToOne, Column, PrimaryGeneratedColumn, ManyToOne, OneToMany } from "typeorm";
import { ModelInterface } from "../model.interface";
import { Escola } from "./escola";
import { Curso } from "./curso";
import { RegistroEscolaRegime } from "./registro-escola-regime";
import { Modulo } from "./modulo";

@Entity()
export class RegistroEscolaCurso implements ModelInterface {

    @PrimaryGeneratedColumn({ type: "bigint" })
    id: number;

    @ManyToOne(type => Curso, curso => curso.listaRegistroCurso)
    curso: Curso;

    @ManyToOne(type => Escola, escola => escola.listaRegistroEscolaCurso)
    escola: Escola;

    @OneToMany(type => RegistroEscolaRegime, registroEscolaRegime => registroEscolaRegime.registroEscolaCurso)
    listaRegistroEscolaRegime: RegistroEscolaRegime[];

    @OneToMany(type => Modulo, modulo => modulo.registroEscolaCurso)
    listaModulo: Modulo[];

    @Column({ type: "date" })
    dataInicio: Date;

    @Column({ type: "date" })
    dataFim: Date;

    @Column({ type: "int" })
    regime: number;

}