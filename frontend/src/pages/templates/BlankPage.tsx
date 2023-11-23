import React from "react";

import { Box, CssBaseline, CssVarsProvider, Stack, Typography } from "@mui/joy";

// Middlewares
import AuthChecker, { AuthCheckerProps } from "../../middlewares/AuthChecker";
import StudentOnly from "../../middlewares/StudentOnly";
import AdminOnly from "../../middlewares/AdminOnly";

// Enums
import { Status } from "../../assets/enums/Status.enum";

// Content of the Template
import Sidebar from "../../components/SideBar";
import Header from "../../components/Header";

export default function BlankPage({
  children,
  role,
  maxContentWidth = "1500px",
  pageTitle,
}: {
  children: any;
  role?: Status | null;
  maxContentWidth?: string;
  pageTitle: string;
}) {
  let RoleVerificationClass: React.FC<AuthCheckerProps>;
  switch (role) {
    case Status.ADMIN:
      RoleVerificationClass = AdminOnly;
      break;
    case Status.STUDENT:
      RoleVerificationClass = StudentOnly;
      break;
    default:
      RoleVerificationClass = AuthChecker;
      break;
  }

  return (
    <AuthChecker>
      <RoleVerificationClass>
        <CssVarsProvider disableTransitionOnChange>
          <CssBaseline />
          <Box sx={{ display: "flex", minHeight: "100dvh" }}>
            <Header />
            <Sidebar />
            <Box
              component="main"
              className="MainContent"
              sx={{
                pt: {
                  xs: "calc(12px + var(--Header-height))",
                  md: 3,
                },
                pb: {
                  xs: 2,
                  sm: 2,
                  md: 3,
                },
                flex: 1,
                display: "flex",
                flexDirection: "column",
                minWidth: 0,
                height: "100dvh",
                gap: 1,
                overflow: "auto",
              }}
            >
              <Box
                sx={{
                  flex: 1,
                  width: "100%",
                }}
              >
                <Box
                  sx={{
                    position: "sticky",
                    top: {
                      sm: -100,
                      md: -110,
                    },
                    bgcolor: "background.body",
                    zIndex: 9995,
                  }}
                >
                  <Box
                    sx={{
                      px: {
                        xs: 2,
                        md: 6,
                      },
                    }}
                  >
                    <Typography
                      level="h2"
                      sx={{
                        mt: 1,
                        mb: 2,
                      }}
                    >
                      {pageTitle}
                    </Typography>
                  </Box>
                </Box>

                <Stack
                  spacing={4}
                  sx={{
                    display: "flex",
                    maxWidth: maxContentWidth,
                    mx: "auto",
                    px: {
                      xs: 2,
                      md: 6,
                    },
                    py: {
                      xs: 2,
                      md: 3,
                    },
                  }}
                >
                  {children}
                </Stack>
              </Box>
            </Box>
          </Box>
        </CssVarsProvider>
      </RoleVerificationClass>
    </AuthChecker>
  );
}
