import { Entity, PrimaryGeneratedColumn, Column, OneToMany, ManyToMany } from "typeorm";
import { RegistroEscolaRegimeMateria } from "./registro-escola-regime-materia";
import { Modulo } from "./modulo";
import { ModelInterface } from "../model.interface";

@Entity()
export class Materia implements ModelInterface {

    @PrimaryGeneratedColumn({type: "bigint"})
    id: number;

    @Column({ type: "text" })
    nome: string;

    @Column({ type: "text" })
    descricao: string;

    @OneToMany(type => RegistroEscolaRegimeMateria, registroEscolaRegimeMateria => registroEscolaRegimeMateria.registroEscolaRegime)
    listaRegistroEscolaRegimeMateria: RegistroEscolaRegimeMateria[];

    @ManyToMany(type => Modulo, modulo => modulo.listaMateria, {
        cascadeInsert: true,
        cascadeUpdate: true
    })
    listaModulo: Modulo[] = [];

}