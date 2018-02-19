class Util {

    public printElement = (htmlElement: HTMLElement | any) => {
        return new Promise((resolve, reject) => {
            let div = document.createElement('div');
            div.classList.add('print-only');
            div.innerHTML = htmlElement.outerHTML;
            document.body.appendChild(div);
            setImmediate(() => {
                this.onPrintFinished(window.print(), div, resolve);
            });
        });
    }

    private onPrintFinished = (printed, div, resolve) => {
        document.body.removeChild(div);
        resolve();
    }

}

export const PrintBrowserUtil = new Util();