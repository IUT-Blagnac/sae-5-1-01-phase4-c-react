import {
  Box,
  Card,
  Chip,
  CssBaseline,
  CssVarsProvider,
  Divider,
  FormControl,
  FormLabel,
  Input,
  Option,
  Select,
  Stack,
  Textarea,
  Typography,
} from "@mui/joy";

import { useState } from "react";
import Sidebar from "../../../components/SideBar";
import AuthChecker from "../../../middlewares/AuthChecker";
import Header from "../../../components/Header";

export default function Support() {
  return (
    <AuthChecker>
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
                    Support
                  </Typography>
                </Box>
              </Box>

              <Stack
                spacing={4}
                sx={{
                  display: "flex",
                  maxWidth: "800px",
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
              ></Stack>
            </Box>
          </Box>
        </Box>
      </CssVarsProvider>
    </AuthChecker>
  );
}
