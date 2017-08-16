import axios from 'axios';
export class Factory {

    private static apiUrl = 'http://localhost:8000';
    protected static joinUrl(url: string) {
        return this.apiUrl + url;
    }

    protected static async post(url: string, body: any) {
         let result = await axios.post(url, body);
         return result.data as any;
    }

    protected static async put(url: string, body: any) {
         let result = await axios.put(url, body);
         return result.data as any;
    }

    protected static async get(url: string, body: any) {
         let result = await axios.get(url, body);
         return result.data as any;
    }
    

}