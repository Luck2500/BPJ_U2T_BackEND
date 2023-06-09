﻿namespace BPJ_U2T.Interfaces
{
    public interface IUploadFileService
    {
        //ตรวจสอบมีการอัพโหลดไฟล์เข้ามาหรือไม่
        bool IsUpload(IFormFileCollection formFiles);
        //ตรวจสอบนามสกุลไฟล์หรือรูปแบบที่่ต้องการ
        string Validation(IFormFileCollection formFiles);
        //อัพโหลดและส่งรายชื่อไฟล์ออกมา
        Task<List<string>> UploadImages(IFormFileCollection formFiles);
        // อัพโหลดและส่งรายชื่อไฟล์ออกมา
        Task<List<string>> UploadVedio(IFormFileCollection formFiles);
        Task DeleteImages(string fileName);
        Task DeleteVedio(string fileName);
    }
}
