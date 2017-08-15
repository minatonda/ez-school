import { Entity, OneToOne, Column, PrimaryGeneratedColumn, ManyToOne, ManyToMany, JoinTable, OneToMany } from "typeorm";
import { ModelInterface } from "../model.interface";
import { Escola } from "./escola";
import { Aluno } from "./aluno";
import { RegistroEscolaRegime } from "./registro-escola-regime";
import { Materia } from "./materia";
import { Aula } from "./aula";
import { Nota } from "./nota";

@Entity()
export class RegistroEscolaRegimeMateria implements ModelInterface {

    @PrimaryGeneratedColumn({type: "bigint"})
    id: number;

    @ManyToOne(type => RegistroEscolaRegime, registroEscolaRegime => registroEscolaRegime.listaRegistroEscolaRegimeMateria)
    registroEscolaRegime: RegistroEscolaRegime;

    @ManyToOne(type => Materia, materia => materia.listaRegistroEscolaRegimeMateria)
    materia: Materia;

    @OneToMany(type => Aula, aula => aula.registroEscolaMateria)
    listaAula: Aula[];

    @OneToMany(type => Nota, nota => nota.registroEscolaMateria)
    listaNota:Nota[];


}