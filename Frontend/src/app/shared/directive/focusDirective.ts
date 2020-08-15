import { AfterContentInit, Directive, ElementRef, Input } from '@angular/core';

@Directive({
  selector: '[autofocus]'
})
export class FocusDirective implements AfterContentInit {
    public constructor(private el: ElementRef) {
    }
    public ngAfterContentInit() {
        setTimeout(() => {
            if(this.el.nativeElement.id==this.el.nativeElement.form.elements[0].id)
            this.el.nativeElement.focus();
        });
    }
}