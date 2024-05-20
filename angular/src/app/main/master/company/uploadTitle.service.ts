import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';

@Injectable({
    providedIn: 'root'
})

export class UploadTitleService {
    uploaderTitle: BehaviorSubject<string> = new BehaviorSubject<string>('Upload Image');
    ratio: BehaviorSubject<number> = new BehaviorSubject<number>(4 / 4);
    aspectRatio: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(true);
    data: Observable<string> = this.uploaderTitle.asObservable();
    ratioData: Observable<number> = this.ratio.asObservable();
    aspectRatioData: Observable<boolean> = this.aspectRatio.asObservable();

    constructor() {
    }

    sendData(data: string) {
        this.uploaderTitle.next(data);
    }

    sendRatio(ratioData: number) {
        this.ratio.next(ratioData);
    }

    sendAspectRatioData(aspectRatioData: boolean) {
        this.aspectRatio.next(aspectRatioData);
    }
}
