class Util {

    public chain(list: Array<any>, chain: number): Array<Array<any>> {
        let retorno = [];
        let sub = [];
        list.forEach((item, index) => {
            sub.push(item);
            if ((index + 1) % chain === 0) {
                retorno.push(sub);
                sub = [];
            }
        });
        return retorno;
    }

}

export const ArrayUtil = new Util();