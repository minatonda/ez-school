import { Entity, OneToOne, Column, PrimaryGeneratedColumn, ManyToOne } from "typeorm";
import { ModelInterface } from "../model.interface";
import { RegistroEscolaRegimeMateria } from "./registro-escola-regime-materia";

@Entity()
export class Nota implements ModelInterface {

    @PrimaryGeneratedColumn({type: "bigint"})
    id: number;

    @ManyToOne(type => RegistroEscolaRegimeMateria, registroEscolaMateria => registroEscolaMateria.listaAula)
    registroEscolaMateria: RegistroEscolaRegimeMateria;

    @Column({ type: "text" })
    observacao: string;

    @Column({ type: "date" })
    dataLancamento: Date;

    @Column({ type: "int" })
    valor: number;
}