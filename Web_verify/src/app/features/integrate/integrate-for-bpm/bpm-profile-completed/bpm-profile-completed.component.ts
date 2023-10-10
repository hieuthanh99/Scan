import {Component, Injector, OnInit, ViewChild, ViewEncapsulation} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import {ComponentAbstract} from "@shared-sm";
import {AlbumService} from "../../services/album.service";
import {finalize, takeUntil} from "rxjs";
import {PdfDto} from "../../dtos/pdf-dto";
import {MatMenuTrigger} from "@angular/material/menu";
import {environment} from "@env/environment";
import {DomSanitizer, SafeResourceUrl} from "@angular/platform-browser";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-bpm-profile-completed',
  templateUrl: './bpm-profile-completed.component.html',
  styleUrls: ['./bpm-profile-completed.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class BpmProfileCompletedComponent extends ComponentAbstract implements OnInit {

  pdfs: PdfDto[] = [];
  pdfChecked: string[] = [];
  menuTopLeftPosition = {x: '0', y: '0'}
  @ViewChild(MatMenuTrigger, {static: true}) contextMenu: MatMenuTrigger;

  pdfSrc: SafeResourceUrl = '';
  pdfName: string = '';
  pdfTime: string = '';

  constructor(protected injector: Injector,
              private formBuilder: FormBuilder,
              private albumService: AlbumService,
              private sanitizer: DomSanitizer,
              private datePipe: DatePipe) {
    super(injector);
    this.form = formBuilder.group({
      find: [''],
    });
  }

  protected componentInit(): void {
    this.findPdf();
  }

  get hasData(): boolean {
    return !!this.pdfs && this.pdfs.length > 0;
  }

  findPdf() {
    this.indicator.showActivityIndicator();
    try {
      const text = this.form.get('find').value;
      this.albumService.findPdf(text).pipe(
        takeUntil(this.ngUnsubscribe),
        finalize(() => this.indicator.hideActivityIndicator())
      ).subscribe((res: any) => {
          if (res && res.status === 200) {
            this.pdfs = res.data.result.content;
            this.pdfs.push(...this.pdfs);
            this.pdfs.push(...this.pdfs);
            this.pdfs.push(...this.pdfs);
          }
        },
        (error) => console.log(error));
    } catch (e) {
      debugger
    }
  }

  clearSearchText() {
    this.form.get('find').setValue('');
    this.findPdf();
  }

  onCheckChange(pdf: PdfDto) {
    const index = this.pdfChecked.indexOf(pdf.path);
    if (index >= 0) {
      this.pdfChecked.splice(index, 1);
    } else {
      this.pdfChecked.push(pdf.path);
    }
  }

  onContextMenu(event: MouseEvent, pdf: PdfDto) {
    event.preventDefault();
    this.menuTopLeftPosition.x = event.clientX + 'px';
    this.menuTopLeftPosition.y = event.clientY + 'px';
    this.contextMenu.menuData = {item: pdf}
    this.contextMenu.openMenu();
  }

  onShowProfile(pdf: PdfDto) {
    this.pdfSrc = this.sanitizer.bypassSecurityTrustResourceUrl(environment.s3Sever + environment.bucketName + pdf.path);
    this.pdfName = pdf.name;
    this.pdfTime = this.datePipe.transform(pdf.createdDate, 'dd/MM/yyyy - HH:mm', '-0');
  }

  onCloseProfile() {
    this.pdfSrc = '';
    this.pdfName = '';
  }
}
