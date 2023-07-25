package SAR0083.Homework;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.authentication.AuthenticationProvider;
import org.springframework.security.authentication.dao.DaoAuthenticationProvider;
import org.springframework.security.config.annotation.method.configuration.EnableGlobalMethodSecurity;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.web.SecurityFilterChain;

import SAR0083.Homework.services.AccountService;

@Configuration
@EnableWebSecurity
@EnableGlobalMethodSecurity(prePostEnabled = true, securedEnabled = true, jsr250Enabled = true)
public class SecurityConfig {
	@Autowired
	private AccountService userDetailsService;
	
	@Bean
	public AuthenticationProvider authProvider() {
		DaoAuthenticationProvider provider = new DaoAuthenticationProvider();
		provider.setUserDetailsService(userDetailsService);
		provider.setPasswordEncoder(new BCryptPasswordEncoder());
		
		return provider;
	}
	
	@Bean
	public SecurityFilterChain filterChain(HttpSecurity http) throws Exception {
		http.csrf()
			.disable()
			.authorizeRequests()
			.antMatchers("/*.css").permitAll()
			.antMatchers("/*.png").permitAll()
			
			.antMatchers("/sign-up/**").permitAll()
			.antMatchers("/sign-in/**").permitAll()
			.antMatchers("/sign-out/**").permitAll()
			
			.antMatchers("/api/**").permitAll()
			
			.antMatchers("/**").hasAuthority("ROLE_USER")
			.anyRequest()
			.authenticated();
		
		http.formLogin()
			.loginPage("/sign-in")
			.usernameParameter("emailAddress")
			.loginProcessingUrl("/sign-in/account");
		
		http.logout()
			.logoutUrl("/sign-out");
		
		return http.build();
	}
}