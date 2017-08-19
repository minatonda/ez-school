import { Factory } from '../factory';
import { Credentials } from './credentials';

export class UsuarioFactory extends Factory {

    public static async autenticar() {
        let credentials = new Credentials();
        credentials.email = 'someuser@somewhere.com';
        credentials.password = '123456#User';
        return await UsuarioFactory.post('/api/account', credentials);
    }


}