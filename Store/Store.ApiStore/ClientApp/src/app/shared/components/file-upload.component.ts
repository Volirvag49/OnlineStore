import { Component, HostListener, ElementRef } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';
import { Observable } from 'rxjs';
import { FileToUploadGetModel } from '../../core';


@Component({
    selector: 'app-file-upload',
    templateUrl: './file-upload.component.html',
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: FileUploadComponent,
            multi: true
        }
    ]
})
export class FileUploadComponent implements ControlValueAccessor {
    onChange: Function;
    imagePreview: string;
    private file: any | null = null;

    MAX_SIZE: number = 1048576;
    SLICE_SIZE = 512
    messages: string[] = [];

    @HostListener('change', ['$event.target.files']) emitFiles(event: FileList) {
        this.messages = [];
        const file = event && event.item(0);
        this.onChange(file);

        this.file = file;

        this.showPreview(this.file)
            .subscribe((img) => {
                if (img != null) {
                    this.imagePreview = img.toString();
                    this.file.fileAsBase64 = img.toString();
                }
            });   
    }

    constructor(private host: ElementRef<HTMLInputElement>) {
    }

    writeValue(value: null) {
        // clear file input
        this.host.nativeElement.value = '';
        this.file = null;
        this.imagePreview = null;
    }

    registerOnChange(fn: Function) {
        this.onChange = fn;
    }

    registerOnTouched(fn: Function) {
    }

    showPreview(file: File): Observable<File> {

        // Don't allow file sizes over 1MB
        if (file.size < this.MAX_SIZE) {
            // Set theFile property
            const reader = new FileReader();
            reader.readAsDataURL(file);

            return Observable.create(observer => {
                reader.onloadend = () => {
                    observer.next(reader.result);
                    observer.complete();
                };
            });
        }
        else {
            this.messages.push("File: " +
                file.name
                + " is too large to upload.");

            return Observable.create(observer => {
                observer.next(null);
                observer.complete();
            });
        }
    }

    showPreviewFromModel(model: FileToUploadGetModel) {
        if (model == null)
            return;
        this.file = this.getImage(model);

        this.showPreview(this.file)
            .subscribe((img) => {
                if (img != null) {
                    this.imagePreview = img.toString();
                    this.file.fileAsBase64 = img.toString();
                }
            });
    }

    private getImage(file: FileToUploadGetModel): Blob {
        if (file == null)
            return null;
        const byteCharacters = atob(file.fileAsBase64
            .replace("data:image/png;base64,", ""));
        const byteArrays = [];
        const contentType = 'image/png';

        for (let offset = 0; offset < byteCharacters.length; offset += this.SLICE_SIZE) {
            const slice = byteCharacters.slice(offset, offset + this.SLICE_SIZE);

            const byteNumbers = new Array(slice.length);
            for (let i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }

            const byteArray = new Uint8Array(byteNumbers);
            byteArrays.push(byteArray);
        }

        const blob = new Blob(byteArrays, { type: contentType });

        return blob;
    }
}
