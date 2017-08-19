import axios from 'axios';
export class Factory {

    private static apiUrl = 'http://localhost:5000';
    protected static joinUrl(url: string) {
        return this.apiUrl + url;
    }

    protected static async post(url: string, body: any) {
         let result = await axios.post(this.joinUrl(url), body);
         return result.data as any;
    }

    protected static async put(url: string, body: any) {
         let result = await axios.put(this.joinUrl(url), body);
         return result.data as any;
    }

    protected static async get(url: string, body: any) {
         let result = await axios.get(this.joinUrl(url), body);
         return result.data as any;
    }
    

}