import toastr from 'toastr';

export class Notify {

    public static error(message: MESSAGES | string, title: string) {
        toastr.error(message, title);
    }

    public static success(message: MESSAGES | string, title: string) {
        toastr.success(message, title);
    }

    public static info(message: MESSAGES | string, title: string) {
        toastr.info(message, title);
    }

    public static warning(message: MESSAGES | string, title: string) {
        toastr.warning(message, title);
    }

    public static notify(message: MESSAGES | string, title: string, type: NOTIFY_TYPE, cancel?: boolean) {
        if (!cancel) {
            switch (type) {
                case (NOTIFY_TYPE.SUCCESS): {
                    this.success(message, title);
                    break;
                }
                case (NOTIFY_TYPE.ERROR): {
                    this.error(message, title);
                    break;
                }
                case (NOTIFY_TYPE.INFO): {
                    this.info(message, title);
                    break;
                }
                case (NOTIFY_TYPE.WARNING): {
                    this.warning(message, title);
                    break;
                }
            }
        }
    }

}

export enum NOTIFY_TYPE {
    SUCCESS = 'SUCCESS',
    ERROR = 'ERROR',
    INFO = 'INFO',
    WARNING = 'WARNING'
}

export enum MESSAGES {
    REGISTRO_NOT_FOUND = 'Registro não encontrado.',
    REGISTRO_ADD = 'Registro salvo com êxito.',
    REGISTRO_ADD_FAIL = 'Falha ao salvar registro',
    REGISTRO_UPD = 'Registro salvo com êxito.',
    REGISTRO_UPD_FAIL = 'Falha ao salvar registro',
    REGISTRO_DEL = 'Registro removido com sucesso.',
    REGISTRO_DEL_FAIL = 'Falha ao remover registro.',
    REGISTRO_GET = 'Registro(s) obtido(s) com êxito.',
    REGISTRO_GET_FAIL = 'Falha ao obter registro(s).',
    LOGIN = 'Login',
    LOGIN_FAIL = 'Falha no login',
    LOGOUT = 'Logout',
    LOGOUT_FAIL = 'Falha no Logout'
}