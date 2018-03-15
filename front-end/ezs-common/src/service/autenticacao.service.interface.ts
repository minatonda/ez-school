import { AutenticacaoModel } from "../model/server/autenticacao.model";

export interface AutenticaoServiceInterface {

    autenticar(autenticao: AutenticacaoModel): void;
    desautenticar(): void;
    isAutenticado(): boolean;
    setAutenticacao(autenticacao: AutenticacaoModel): void;
    getAutenticacao(): AutenticacaoModel;
    getToken(): string;

}