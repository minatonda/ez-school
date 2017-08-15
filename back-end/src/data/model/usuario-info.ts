import { Entity, OneToOne, PrimaryGeneratedColumn, Column, JoinColumn } from "typeorm";
import { ModelInterface } from "../model.interface";
import { Usuario } from "./usuario";

@Entity()
export class UsuarioInfo implements ModelInterface {

    @PrimaryGeneratedColumn({ type: "bigint" })
    id: number;

    @Column({ type: "text" })
    nome: string;

    @Column({ type: "date" })
    dataNascimento: Date;

    @Column({ type: "text" })
    rg: string;

    @Column({ type: "text" })
    cpf: string;

    @OneToOne(type => Usuario)
    @JoinColumn()
    usuario: Usuario;

}