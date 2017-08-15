import { Entity, OneToOne, PrimaryGeneratedColumn, Column, JoinColumn } from "typeorm";
import { ModelInterface } from "../model.interface";
import { UsuarioInfo } from "./usuario-info";

@Entity()
export class Usuario implements ModelInterface {

    @PrimaryGeneratedColumn({type: "bigint"})
    id: number;

    @Column({ type: "text" })
    nomeusuario: string;

    @Column({ type: "text" })
    senha: string;

    @OneToOne(type => UsuarioInfo)
    @JoinColumn()
    usuarioInfo: UsuarioInfo;
    
}