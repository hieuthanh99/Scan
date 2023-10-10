import { Component, Injector , TemplateRef, ViewEncapsulation} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ComponentAbstract } from "@shared-sm";
import { album } from 'src/app/models/album';
import { AlbumService } from '../../services/album.service';
import { image } from 'src/app/models/image';
import { environment } from '@env/environment';
import { Subject, debounceTime, distinctUntilChanged } from 'rxjs';


@Component({
  selector: 'app-bpm-complete-profile',
  templateUrl: './bpm-complete-profile.component.html',
  styleUrls: ['./bpm-complete-profile.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class BpmCompleteProfileComponent extends ComponentAbstract {
  s3Url: string;
  stt: number;
  totalText: number = 0;
  index: 0;
  albums: album[] = [];
  album: album = {
    id: '',
    name: '',
    path: '',
    numberOfImage: 0,
    createdDate: '',
    modifiedDate: '',
    createdByUser: '',
    modifiedByUser: '',
    ownerUser: '',
    thumbnailImage: '',
    priorityDisplay: '',
    images: [],
    customerType: '',
  }
  imagePdf: string[] = [];
  images: image[] = [];
  nameAlbum: string = '';
  textTotal = new Subject<string>();

  src : string = '';

  constructor(
    private albumService: AlbumService,
    protected dialog: MatDialog,
    protected injector: Injector

  ) {
    super(injector);
  }


  protected componentInit(): void {
    this.form = this.itemControl.toFormGroup([]);
    this.findAll();
    // this.viewImageS3("/smartcapture/NguyenVanHoangTuanAnh.01/NguyenVanHoangTuanAnh.01_001.jpeg");
    this.s3Url = environment.s3Sever + environment.bucketName;
    this.textTotal.pipe(
      debounceTime(100),
      distinctUntilChanged(),
    ).subscribe((key : string)=>{
      if (key.length === 0) {
        this.totalText = 0;
      }else{
        this.totalText = key.length;
      }
    })
  }

  getFullUrlImage(url): string {
    return environment.smartScanApi + '/sscan-rs/v2.0/albums/viewFileS3?path=/smartcapture/NguyenVanHoangTuanAnh.01/NguyenVanHoangTuanAnh.01_001.jpeg' + url;
  }

  findAll() {
    this.albumService.getAllActiveAlbum().subscribe({
      next: (data) => {
        this.albums = data.data.result.content;
      }
    })
  }

  findAlbumById(e: Event) {
    for (let index = 0; index < this.albums.length; index++) {
      if ((e.target as HTMLInputElement).value === this.albums[index].id) {
        this.images = this.albums[index].images;
        this.nameAlbum = this.albums[index].name;
        this.imagePdf = [];
      }
    }
    for (let index = 0; index < this.images.length; index++) {
      if (this.images[index].status != 'IN_USER' || this.images[index].sync != true) {
        this.images.splice(index, 1);
      }
    }
  }

  viewImageS3(path : string){
    this.albumService.viewImageS3(path).subscribe(
      data  =>{
        this.displayImage(data.body);
      }
    )
  }

  displayImage(blob: Blob) {
    const urlCreator = window.URL || window.webkitURL;
    this.src = urlCreator.createObjectURL(blob);
  }



  checkedImage( imageCheck : image){
    const index = this.imagePdf.indexOf(imageCheck.path);
    if (index >= 0) {
      this.imagePdf.splice(index, 1);
      console.log(this.imagePdf)
    } else {
      this.imagePdf.push(imageCheck.path);
      console.log(this.imagePdf);
    }
  }

  openDialog(templateRef: TemplateRef<any>){
    this.dialog.open(templateRef);
  }

  closeDialog(){
    this.dialog.closeAll();
  }

  countText(content: string){
    this.textTotal.next(content);
  }

}

