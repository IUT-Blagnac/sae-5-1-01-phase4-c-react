import React, { useState } from "react";
import {
  Card,
  CardContent,
  Typography,
  Button,
  SvgIcon,
  styled,
} from "@mui/joy";
import { Status } from "../../../assets/enums/Status.enum";
import BlankPage from "../../templates/BlankPage";

const VisuallyHiddenInput = styled("input")`
  clip: rect(0 0 0 0);
  clip-path: inset(50%);
  height: 1px;
  overflow: hidden;
  position: absolute;
  bottom: 0;
  left: 0;
  white-space: nowrap;
  width: 1px;
`;

export default function ImportUser() {
  const [file, setFile] = useState<File | null>(null);

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.files) {
      setFile(event.target.files[0]);
    }
  };

  const handleImport = () => {
    if (file) {
      console.log(`Importing file: ${file.name}`);
    }
  };

  return (
    <BlankPage
      pageTitle="Importer des utilisateurs"
      role={Status.ADMIN}
      maxContentWidth="650px"
    >
      <Card>
        <CardContent>
          <Typography level="title-sm" component="div" gutterBottom>
            Importer un fichier CSV
          </Typography>
          <Typography level="body-sm">{file ? file.name : ""}</Typography>
          <Button
            component="label"
            role={undefined}
            tabIndex={-1}
            variant="outlined"
            color="neutral"
            startDecorator={
              <SvgIcon>
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  fill="none"
                  viewBox="0 0 24 24"
                  strokeWidth={1.5}
                  stroke="currentColor"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    d="M12 16.5V9.75m0 0l3 3m-3-3l-3 3M6.75 19.5a4.5 4.5 0 01-1.41-8.775 5.25 5.25 0 0110.233-2.33 3 3 0 013.758 3.848A3.752 3.752 0 0118 19.5H6.75z"
                  />
                </svg>
              </SvgIcon>
            }
          >
            Choisir un fichier
            <VisuallyHiddenInput
              type="file"
              accept=".csv"
              onChange={handleFileChange}
            />
          </Button>
          <Button
            color="primary"
            style={{ marginTop: "16px" }}
            disabled={!file}
            onClick={handleImport}
          >
            Importer
          </Button>
        </CardContent>
      </Card>
    </BlankPage>
  );
}
