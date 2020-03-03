import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { FormGroup, FormBuilder } from '@angular/forms';

import { SphereForm } from '@app/models/sphere/sphere-form';

@Component({
  selector: 'app-sphere-dialog',
  templateUrl: './sphere-dialog.component.html'
})
export class SphereDialogComponent implements OnInit {

  dialogTitle: string;

  sphereForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<SphereDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data) {
    const sphereForm: SphereForm = {
      name: data.name,
      description: data.description
    };
    this.createForm(sphereForm);
    this.dialogTitle = data.dialogTitle;
  }

  ngOnInit() {}

  createForm(sphereForm: SphereForm): void {
    this.sphereForm = this.formBuilder.group(sphereForm);
  }

  close(): void {
    this.dialogRef.close();
  }

  save(): void {
    const result: SphereForm = Object.assign({}, this.sphereForm.value);
    this.dialogRef.close({ name: result.name, description: result.description });
  }

}
